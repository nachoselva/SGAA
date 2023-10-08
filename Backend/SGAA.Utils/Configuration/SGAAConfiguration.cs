namespace SGAA.Utils.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class SGAAConfiguration : ISGAAConfiguration
    {
        private const string JWT = "Jwt";

        private readonly IConfiguration _configuration;
        public SGAAConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            Jwt = new JwtConfiguration(_configuration.GetSection(JWT));
        }

        public IJwtConfiguration Jwt { get; }

        public string GetDatabaseConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }
    }

    public class JwtConfiguration : IJwtConfiguration
    {
        private const string ISSUER = nameof(Issuer);
        private const string AUDIENCE = nameof(Audience);
        private const string KEY = nameof(Key);
        private const string REFRESHTOKENVALIDITYINDAYS = nameof(RefreshTokenValidityInDays);
        private const string TOKENVALIDITYINMINUTES = nameof(TokenValidityInMinutes);
        private readonly IConfiguration _configuration;
        public JwtConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;

            Issuer = _configuration[ISSUER] ?? string.Empty;
            Audience = _configuration[AUDIENCE] ?? string.Empty;
            Key = _configuration[KEY] ?? string.Empty;

            if (int.TryParse(_configuration[REFRESHTOKENVALIDITYINDAYS], out int refreshTokenValidityInDays))
            {
                RefreshTokenValidityInDays = refreshTokenValidityInDays;
            }
            if (int.TryParse(_configuration[TOKENVALIDITYINMINUTES], out int tokenValidityInMinutes))
            {
                TokenValidityInMinutes = tokenValidityInMinutes;
            }
        }

        public string Issuer { get; }

        public string Audience { get; }

        public string Key { get; }

        public int RefreshTokenValidityInDays { get; }

        public int TokenValidityInMinutes { get; }
    }
}
