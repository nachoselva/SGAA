namespace SGAA.Utils.Configuration
{
    public interface ISGAAConfiguration
    {
        IJwtConfiguration Jwt { get; }
        ISmtpConfiguration Smtp { get; }
        IFrontendConfiguration Frontend { get; }
        string GetDatabaseConnectionString();
    }
    public interface IJwtConfiguration
    {
        string Issuer { get; }
        string Audience { get; }
        string Key { get; }
        int RefreshTokenValidityInDays { get; }
        int TokenValidityInMinutes { get; }
    }

    public interface ISmtpConfiguration
    {
        public string Host { get; }
        public int Port { get; }
        public string Username { get; }
        public string Password { get; }
        public bool EnableSsl { get; }
    }

    public interface IFrontendConfiguration
    {
        public string Url { get; }
    }
}
