namespace SGAA.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RefreshTokenPostModel
    {
        [Required]
        public required string AccessToken { get; set; }
        [Required]
        public required string RefreshToken { get; set; }
    }
}
