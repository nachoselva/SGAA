namespace SGAA.Models
{
    public class RefreshTokenGetModel
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
