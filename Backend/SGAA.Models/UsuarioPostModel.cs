namespace SGAA.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UsuarioPostModel
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
