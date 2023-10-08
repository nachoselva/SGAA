namespace SGAA.Domain.Auth
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Domain.Base;

    public class UsuarioToken : IdentityUserToken<int>, IAuditableEntity
    {
        public Usuario Usuario { get; set; } = default!;
        public Audit Audit { get; set; } = default!;
    }
}
