namespace SGAA.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RevokeTokenPostModel
    {
        [Required]
        public required string Email { get; set; }
    }
}
