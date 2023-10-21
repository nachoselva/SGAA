namespace SGAA.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordPostModel
    {
        [Required]
        public required string Email { get; set; }
    }
}
