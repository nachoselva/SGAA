namespace SGAA.Utils.Configuration
{
    public interface ISGAAConfiguration
    {
        IJwtConfiguration Jwt { get; }
        string GetConnectionString(string name);
    }
    public interface IJwtConfiguration
    {
        string Issuer { get; }
        string Audience { get; }
        string Key { get; }
        int RefreshTokenValidityInDays { get; }
        int TokenValidityInMinutes { get; }
    }
}
