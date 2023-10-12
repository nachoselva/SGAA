namespace SGAA.Service
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using SGAA.Utils.Configuration;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    public class UsuarioService : UserManager<Usuario>, ISecurityService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISGAAConfiguration _configuration;
        private readonly IUsuarioMapper _usuarioMapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, ISGAAConfiguration configuration, IUsuarioMapper usuarioMapper,
            IUserStore<Usuario> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Usuario> passwordHasher,
            IEnumerable<IUserValidator<Usuario>> userValidators, IEnumerable<IPasswordValidator<Usuario>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services,
            ILogger<UserManager<Usuario>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
                  logger)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _usuarioMapper = usuarioMapper;
        }

        private JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Jwt.Key));

            var token = new JwtSecurityToken(
                issuer: _configuration.Jwt.Issuer,
                audience: _configuration.Jwt.Audience,
                expires: DateTime.Now.AddMinutes(_configuration.Jwt.TokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Jwt.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        private BadRequestException MapIdentityErrorToBadRequest(IEnumerable<IdentityError> errors)
        {
            BadRequestException badRequestException = new();
            foreach (IdentityError error in errors)
            {
                string fieldName = error.Code switch
                {
                    "InvalidUserName" or "InvalidEmail" or "DuplicateUserName" or "DuplicateEmail" => nameof(UsuarioPostModel.Email),
                    "InvalidRoleName" or "DuplicateRoleName" => nameof(Rol),
                    "UserAlreadyHasPassword" or "UserLockoutNotEnabled" or "UserAlreadyInRole" or "UserNotInRole" or "ConcurrencyFailure" or "InvalidToken" or "LoginAlreadyAssociated" => nameof(Usuario),
                    "PasswordTooShort" or "PasswordRequiresNonAlphanumeric" or "PasswordRequiresDigit" or "PasswordRequiresLower" or "PasswordRequiresUpper" or "PasswordMismatch" => nameof(UsuarioPostModel.Password),
                    _ => "Unknown",
                };
                badRequestException.AddMessage(fieldName, error.Description);
            }
            return badRequestException;
        }
        public async Task<UsuarioGetModel> AddUsuario(UsuarioPostModel model)
        {
            Usuario? sameEmailUsusario = await FindByNameAsync(model.Email);
            if (sameEmailUsusario is not null)
                throw new BadRequestException(nameof(model.Email), "El email está en uso");

            Usuario usuario = model.ToEntity(_usuarioMapper);

            IdentityResult addUserResult = await CreateAsync(usuario, model.Password);
            if (!addUserResult.Succeeded)
            {
                throw MapIdentityErrorToBadRequest(addUserResult.Errors);
            }
            IdentityResult addToRoleResult = await AddToRoleAsync(usuario, model.Rol.ToString());
            if (!addToRoleResult.Succeeded)
            {
                throw MapIdentityErrorToBadRequest(addToRoleResult.Errors);
            }

            return _usuarioMapper.ToGetModel(usuario);
        }

        public async Task<UsuarioGetModel> AddUsuarioPublic(UsuarioPostModel model)
        {
            if (model.Rol == RolType.Administrador)
            {
                throw new UnauthorizedException();
            }
            return await AddUsuario(model);
        }

        public async Task<UsuarioGetModel> Update(int usuarioId, UsuarioPutModel model)
        {
            Usuario? usuario = await FindByIdAsync(usuarioId.ToString());
            if (usuario == null)
                throw new NotFoundException();
            usuario = _usuarioMapper.ToEntity(model, usuario!);
            IdentityResult result = await UpdateAsync(usuario);
            if (!result.Succeeded)
            {
                throw MapIdentityErrorToBadRequest(result.Errors);
            }
            return _usuarioMapper.ToGetModel(usuario);
        }

        public async Task Delete(int usuarioId)
        {
            Usuario? usuario = await FindByIdAsync(usuarioId.ToString());
            if (usuario == null)
                throw new NotFoundException();
            IdentityResult result = await DeleteAsync(usuario);
            if (!result.Succeeded)
            {
                throw MapIdentityErrorToBadRequest(result.Errors);
            }
        }

        public async Task<UsuarioGetModel> AddFirstUsuario(UsuarioPostModel model)
        {
            IReadOnlyCollection<Usuario> members = await _usuarioRepository.GetAllUsuarios();
            if (members.Any())
            {
                throw new UnauthorizedException();
            }
            else
            {
                return await AddUsuario(model);
            }
        }

        public async Task<TokenGetModel> GetToken(UsuarioLoginPostModel model)
        {
            string email = model.Email;
            Usuario? member = await FindByEmailAsync(email);
            if (member != null && await CheckPasswordAsync(member, model.Password))
            {
                var userRoles = await GetRolesAsync(member);

                var authClaims = new List<Claim>
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim("MemberId", member.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, email ),
                    new Claim(JwtRegisteredClaimNames.Email, email ),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                member.RefreshToken = refreshToken;
                member.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.Jwt.RefreshTokenValidityInDays);

                await UpdateAsync(member);

                return new TokenGetModel()
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    Email = member.Email!,
                    FirstName = member.Nombre,
                    LastName = member.Apellido
                };
            }
            throw new UnauthorizedException();
        }

        public async Task<RefreshTokenGetModel> RefreshToken(RefreshTokenPostModel tokenModel)
        {
            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken) ?? throw new BadRequestException(nameof(tokenModel.AccessToken), "Invalid access token or refresh token");
            string? username = principal?.Identity?.Name;

            var user = !string.IsNullOrWhiteSpace(username) ? await FindByNameAsync(username) : null;

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new BadRequestException(nameof(tokenModel.AccessToken), "Invalid access token or refresh token");
            }

            IEnumerable<Claim> claims = principal?.Claims ?? Enumerable.Empty<Claim>();
            var newAccessToken = CreateToken(claims);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await UpdateAsync(user);

            return new RefreshTokenGetModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }

        public async Task Revoke(RevokeTokenPostModel model)
        {
            var user = await FindByNameAsync(model.Email) ?? throw new BadRequestException(nameof(model.Email), "Invalid email");
            user.RefreshToken = null;
            await UpdateAsync(user);
        }

        public async Task RevokeAll()
        {
            var users = Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await UpdateAsync(user);
            }
        }
        public async Task<UsuarioGetModel?> GetById(int id)
        {
            Usuario? usuario = await FindByIdAsync(id.ToString());
            if (usuario == null)
                return null;
            return _usuarioMapper.ToGetModel(usuario);
        }

        public async Task<UsuarioGetModel?> GetByEmail(string email)
        {
            Usuario? usuario = await FindByNameAsync(email);
            if (usuario == null)
                return null;
            return _usuarioMapper.ToGetModel(usuario);
        }
    }
}
