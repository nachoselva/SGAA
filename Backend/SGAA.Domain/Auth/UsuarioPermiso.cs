namespace SGAA.Domain.Auth
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Domain.Base;

    public class UsuarioPermiso : IdentityUserClaim<int>, IEntity
    {
        public Usuario Usuario { get; set; } = default!;
        public Audit Audit { get; set; } = default!;
    }
}
