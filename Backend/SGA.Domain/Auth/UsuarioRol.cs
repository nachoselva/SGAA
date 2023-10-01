namespace SGAA.Domain.Auth
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Domain.Base;

    public class UsuarioRol : IdentityUserRole<int>, IAuditableEntity
    {
        public Rol Rol { get; set; } = default!;
        public Usuario Usuario { get; set; } = default!;
        public Audit Audit { get; set; } = default!;
    }
}
