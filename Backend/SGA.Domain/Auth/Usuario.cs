namespace SGAA.Domain.Auth
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Domain.Base;
    using SGAA.Domain.Core;
    using System;

    public class Usuario : IdentityUser<int>, IEntity
    {
        public Usuario(string firstName, string lastName, string refreshToken, DateTime? refreshTokenExpiryTime)
        {
            FirstName = firstName;
            LastName = lastName;
            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public Audit Audit { get; set; } = default!;
        public IReadOnlyCollection<UsuarioPermiso> UsuarioPermisos { get; set; } = Array.Empty<UsuarioPermiso>();
        public IReadOnlyCollection<UsuarioLogin> UsuarioLogins { get; set; } = Array.Empty<UsuarioLogin>();
        public IReadOnlyCollection<UsuarioToken> UsuarioTokens { get; set; } = Array.Empty<UsuarioToken>();
        public IReadOnlyCollection<UsuarioRol> UsuarioRoles { get; set; } = Array.Empty<UsuarioRol>();
        public IReadOnlyCollection<Aplicacion> Aplicaciones { get; set; } = Array.Empty<Aplicacion>();
        public IReadOnlyCollection<Unidad> Unidades { get; set; } = Array.Empty<Unidad>();
        public IReadOnlyCollection<Firma> Firmas { get; private set; } = Array.Empty<Firma>();
    }
}
