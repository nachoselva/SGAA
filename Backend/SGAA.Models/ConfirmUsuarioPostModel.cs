namespace SGAA.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ConfirmUsuarioPostModel
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Token { get; set; }
    }
}
