namespace SGAA.Domain.Auth
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Domain.Base;
    using System.Collections.Generic;

    public class Rol : IdentityRole<int>, IEntity
    {
        public Rol(int id, RolType rolType, string name, string normalizedName)
        {
            Id = id;
            RolType = rolType;
            Name = name;
            NormalizedName = normalizedName;
        }

        public RolType RolType { get; private set; }

        public IReadOnlyCollection<UsuarioRol> UsuarioRoles { get; } = Array.Empty<UsuarioRol>();
        public IReadOnlyCollection<RolPermiso> Permisos { get; } = Array.Empty<RolPermiso>();
        public Audit Audit { get; } = default!;
    }

    public enum RolType
    {
        Administrator = 1,
        Resident,
        Homeowner
    }
}
