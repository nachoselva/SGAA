namespace SGAA.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UsuarioGetModel
    {
        [Required]
        public required int Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Email { get; set; }
    }
}
