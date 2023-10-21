namespace SGAA.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordPostModel
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Token { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
