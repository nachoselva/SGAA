namespace SGAA.Models
{
    using System;

    public class TokenGetModel
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime Expiration { get; set; }
        public required string Email { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required IList<string> Roles { get; set; }
    }
}
