namespace SGAA.Service
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Errors;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
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
    using System.Web;

    public class UsuarioService : UserManager<Usuario>, ISecurityService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISGAAConfiguration _configuration;
        private readonly IUsuarioMapper _usuarioMapper;
        private readonly IConfirmationEmailSender _emailSender;
        private readonly IResetPasswordEmailSender _resetPasswordEmailSender;

        public UsuarioService(IUsuarioRepository usuarioRepository, ISGAAConfiguration configuration, IUsuarioMapper usuarioMapper,
            IConfirmationEmailSender emailSender, IResetPasswordEmailSender resetPasswordEmailSender, IUserStore<Usuario> store,
            IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Usuario> passwordHasher, IEnumerable<IUserValidator<Usuario>> userValidators,
            IEnumerable<IPasswordValidator<Usuario>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<Usuario>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
                  logger)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _usuarioMapper = usuarioMapper;
            _emailSender = emailSender;
            _resetPasswordEmailSender = resetPasswordEmailSender;
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

        public async Task<UsuarioGetModel> AddUsuario(UsuarioPostModel model)
        {
            Usuario? sameEmailUsusario = await FindByNameAsync(model.Email);
            if (sameEmailUsusario is not null)
                throw new BadRequestException(nameof(model.Email), "El email está en uso");

            Usuario usuario = model.ToEntity(_usuarioMapper);

            IdentityResult addUserResult = await CreateAsync(usuario, model.Password);
            if (!addUserResult.Succeeded)
            {
                throw this.MapIdentityErrorToBadRequest(addUserResult.Errors);
            }
            IdentityResult addToRoleResult = await AddToRoleAsync(usuario, model.Rol.ToString());
            if (!addToRoleResult.Succeeded)
            {
                throw this.MapIdentityErrorToBadRequest(addToRoleResult.Errors);
            }

            string userToken = await GenerateUserTokenAsync(usuario, TokenOptions.DefaultProvider, ConfirmEmailTokenPurpose);
            string confirmationURL = $"{_configuration.Frontend.Url}/auth/confirmar-correo?email={HttpUtility.UrlEncode(usuario.Email)}&token={HttpUtility.UrlEncode(userToken)}";

            await _emailSender.SendEmail(usuario.Email!, new ConfirmationEmailModel()
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                ConfirmationURL = confirmationURL
            });

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

        public async Task<UsuarioGetModel> UpdateUsuario(int usuarioId, UsuarioPutModel model)
        {
            Usuario? usuario = await FindByIdAsync(usuarioId.ToString()) ?? throw new NotFoundException();
            usuario = _usuarioMapper.ToEntity(model, usuario!);
            IdentityResult result = await UpdateAsync(usuario);
            if (!result.Succeeded)
            {
                throw this.MapIdentityErrorToBadRequest(result.Errors);
            }
            return _usuarioMapper.ToGetModel(usuario);
        }

        public async Task DeleteUsuario(int usuarioId)
        {
            Usuario? usuario = await FindByIdAsync(usuarioId.ToString()) ?? throw new NotFoundException();
            IdentityResult result = await DeleteAsync(usuario);
            if (!result.Succeeded)
            {
                throw this.MapIdentityErrorToBadRequest(result.Errors);
            }
        }

        public async Task<UsuarioGetModel> AddFirstUsuario(UsuarioPostModel model)
        {
            IReadOnlyCollection<Usuario> members = await _usuarioRepository.GetUsuarios();
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
            if (member != null && member.EmailConfirmed && await CheckPasswordAsync(member, model.Password))
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
                    Nombre = member.Nombre,
                    Apellido = member.Apellido,
                    Roles = userRoles
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
        public async Task<UsuarioGetModel> GetUsuario(int usuarioId)
        {
            Usuario? usuario = await FindByIdAsync(usuarioId.ToString());
            return usuario == null ? throw new NotFoundException() : _usuarioMapper.ToGetModel(usuario);
        }

        public async Task<UsuarioGetModel?> GetUsuario(string email)
        {
            Usuario? usuario = await _usuarioRepository.GetUsuarioByEmail(email);
            if (usuario == null)
                return null;
            return _usuarioMapper.ToGetModel(usuario);
        }

        public async Task ConfirmUsuario(ConfirmUsuarioPostModel model)
        {
            Usuario? usuario = await FindByNameAsync(model.Email);
            if (usuario != null)
            {
                IdentityResult result = await ConfirmEmailAsync(usuario, model.Token);
                if (!result.Succeeded)
                {
                    throw this.MapIdentityErrorToBadRequest(result.Errors);
                }
            }
            else
            {
                throw new UnauthorizedException();
            }
        }

        public async Task<UsuarioGetModel> ResetPassword(ResetPasswordPostModel model)
        {
            Usuario? usuario = await FindByNameAsync(model.Email);
            if (usuario != null)
            {
                IdentityResult resetPasswordResult = await ResetPasswordAsync(usuario, model.Token, model.Password);
                if (!resetPasswordResult.Succeeded)
                {
                    throw this.MapIdentityErrorToBadRequest(resetPasswordResult.Errors);
                }
                if (!usuario.EmailConfirmed)
                {
                    usuario.EmailConfirmed = true;
                    IdentityResult confirmResult = await UpdateAsync(usuario);
                    if (!confirmResult.Succeeded)
                    {
                        throw this.MapIdentityErrorToBadRequest(confirmResult.Errors);
                    }
                }
            }
            else
            {
                throw new UnauthorizedException();
            }
            return usuario.MapToGetModel(_usuarioMapper);
        }

        public async Task ForgotPassword(ForgotPasswordPostModel model)
        {
            Usuario? usuario = await FindByNameAsync(model.Email);
            if (usuario != null)
            {
                string resetPasswordToken = await GeneratePasswordResetTokenAsync(usuario);
                string resetPasswordURL = $"{_configuration.Frontend.Url}/auth/resetear-password?email={HttpUtility.UrlEncode(usuario.Email)}&token={HttpUtility.UrlEncode(resetPasswordToken)}";
                await _resetPasswordEmailSender.SendEmail(usuario.Email!,
                    new ResetPasswordEmailModel
                    {
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        ResetPasswordURL = resetPasswordURL
                    });
            }
        }

        public async Task<IReadOnlyCollection<UsuarioGetModel>> GetUsuarios()
        {
            IReadOnlyCollection<Usuario> usuarios = await _usuarioRepository.GetUsuarios();
            return usuarios.Select(usuario => usuario.MapToGetModel(_usuarioMapper)).ToList();
        }
    }
}
