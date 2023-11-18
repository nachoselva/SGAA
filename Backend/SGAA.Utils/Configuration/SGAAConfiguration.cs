namespace SGAA.Utils.Configuration
{
    using Microsoft.Extensions.Configuration;

    public class SGAAConfiguration : ISGAAConfiguration
    {
        private const string JWT = nameof(Jwt);
        private const string SMTP = nameof(Smtp);
        private const string FRONTEND = nameof(Frontend);

        private readonly IConfiguration _configuration;
        public SGAAConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            Jwt = new JwtConfiguration(_configuration.GetSection(JWT));
            Smtp = new SmtpConfiguration(_configuration.GetSection(SMTP));
            Frontend = new FrontendConfiguration(_configuration.GetSection(FRONTEND));
        }

        public IJwtConfiguration Jwt { get; }
        public ISmtpConfiguration Smtp { get; }
        public IFrontendConfiguration Frontend { get; }

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

    public class SmtpConfiguration : ISmtpConfiguration
    {
        private const string HOST = nameof(Host);
        private const string PORT = nameof(Port);
        private const string USERNAME = nameof(Username);
        private const string PASSWORD = nameof(Password);
        private const string ENABLESSL = nameof(EnableSsl);

        private readonly IConfiguration _configuration;
        public SmtpConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
            Host = _configuration[HOST] ?? string.Empty;
            if (int.TryParse(_configuration[PORT], out int port))
            {
                Port = port;
            }
            Username = _configuration[USERNAME] ?? string.Empty;
            Password = _configuration[PASSWORD] ?? string.Empty;
            if (bool.TryParse(_configuration[ENABLESSL], out bool enableSSL))
            {
                EnableSsl = enableSSL;
            }
        }

        public string Host { get; }

        public int Port { get; }

        public string Username { get; }

        public string Password { get; }

        public bool EnableSsl { get; }
    }

    public class FrontendConfiguration : IFrontendConfiguration
    {
        private const string URL = nameof(Url);

        private readonly IConfiguration _configuration;
        public FrontendConfiguration(IConfigurationSection configuration)
        {
            _configuration = configuration;
            Url = _configuration[URL] ?? string.Empty;
        }

        public string Url { get; }
    }
}
