namespace SGAA.Domain.Auth
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Domain.Base;

    public class RolPermiso : IdentityRoleClaim<int>, IEntity
    {
        public Rol Rol { get; set; } = default!;
        public Audit Audit { get; set; } = default!;
    }
}
