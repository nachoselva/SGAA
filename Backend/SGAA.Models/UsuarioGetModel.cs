namespace SGAA.Models
{
    using SGAA.Domain.Auth;
    using SGAA.Models.Base;
    using System.ComponentModel.DataAnnotations;

    public class UsuarioGetModel : IGetModel<Usuario>
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
