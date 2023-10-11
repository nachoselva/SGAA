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

        public IReadOnlyCollection<UsuarioRol> UsuarioRoles { get; } = new List<UsuarioRol>();
        public IReadOnlyCollection<RolPermiso> Permisos { get; } = new List<RolPermiso>();
        public Audit Audit { get; set; } = default!;
    }

    public enum RolType
    {
        Administrador = 1,
        Inquilino,
        Propietario
    }
}
