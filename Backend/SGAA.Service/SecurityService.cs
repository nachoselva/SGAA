namespace SGAA.Service
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Service.Contracts;
    using SGAA.Utils.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    public class SecurityService : ISecurityService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ISGAAConfiguration _configuration;
        //private readonly IUsuarioService _memberService;

        public SecurityService(ISGAAConfiguration configuration, UserManager<Usuario> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            //_memberService = memberService;
        }

        public async Task<TokenGetModel> GetToken(UsuarioLoginPostModel model)
        {
            string email = model.Email;
            Usuario? member = await _userManager.FindByEmailAsync(email);
            if (member != null && await _userManager.CheckPasswordAsync(member, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(member);

                var authClaims = new List<Claim>
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim("MemberId", member.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, email ),
                    new Claim("Email", email ),
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

                await _userManager.UpdateAsync(member);

                return new TokenGetModel()
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    Email = member.Email!,
                    FirstName = member.FirstName,
                    LastName = member.LastName
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

            var user = !string.IsNullOrWhiteSpace(username) ? await _userManager.FindByNameAsync(username) : null;

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new BadRequestException(nameof(tokenModel.AccessToken), "Invalid access token or refresh token");
            }

            IEnumerable<Claim> claims = principal?.Claims ?? Enumerable.Empty<Claim>();
            var newAccessToken = CreateToken(claims);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new RefreshTokenGetModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }

        public async Task Revoke(RevokeTokenPostModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email) ?? throw new BadRequestException(nameof(model.Email), "Invalid email");
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
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

        //public Task<UsuarioGetModel> FirstMember(UsuarioPostModel model)
        //{
        //    UsuarioGetModel member = null; // await _memberService.GetByFilter(new UsuarioSearchModel());
        //    if (member != null)
        //    {
        //        throw new UnauthorizedException();
        //    }
        //    else
        //    {
        //        return null;
        //        //return await _memberService.Add(model);
        //    }
        //}
    }
}
