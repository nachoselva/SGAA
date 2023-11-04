namespace SGAA.Models
{
    using SGAA.Domain.Auth;
    using SGAA.Models.Base;

    public class UsuarioGetModel : IGetModel<Usuario>
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string Roles { get; set; }
        public required Licencia Licencia { get; set; }
    }
}
