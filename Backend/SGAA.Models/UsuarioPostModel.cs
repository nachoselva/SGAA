namespace SGAA.Models
{
    using SGAA.Domain.Auth;
    using SGAA.Models.Base;
    using System.ComponentModel.DataAnnotations;

    public class UsuarioPostModel : IPostModel<Usuario>
    {
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required RolType[] Roles { get; set; }
    }
}
