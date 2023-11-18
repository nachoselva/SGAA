namespace SGAA.Models
{
    using SGAA.Domain.Auth;
    using SGAA.Models.Base;
    using System.ComponentModel.DataAnnotations;

    public class UsuarioPutModel : IPutModel<Usuario>
    {
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido { get; set; }
    }
}
