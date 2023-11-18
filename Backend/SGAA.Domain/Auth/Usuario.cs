namespace SGAA.Domain.Auth
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Domain.Base;
    using SGAA.Domain.Core;
    using System;

    public class Usuario : IdentityUser<int>, IEntity
    {
        public Usuario(string email, string nombre, string apellido, string? securityStamp, string? refreshToken, DateTime? refreshTokenExpiryTime, Licencia licencia)
        {
            Email = email;
            Nombre = nombre;
            Apellido = apellido;
            SecurityStamp = securityStamp;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
            Licencia = licencia;
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public Licencia Licencia { get; set; }

        public Audit Audit { get; set; } = default!;
        public IReadOnlyCollection<UsuarioPermiso> UsuarioPermisos { get; set; } = new List<UsuarioPermiso>();
        public IReadOnlyCollection<UsuarioLogin> UsuarioLogins { get; set; } = new List<UsuarioLogin>();
        public IReadOnlyCollection<UsuarioToken> UsuarioTokens { get; set; } = new List<UsuarioToken>();
        public IReadOnlyCollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
        public IReadOnlyCollection<Aplicacion> Aplicaciones { get; set; } = new List<Aplicacion>();
        public IReadOnlyCollection<Unidad> Unidades { get; set; } = new List<Unidad>();
        public IReadOnlyCollection<Firma> Firmas { get; private set; } = new List<Firma>();

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}
