namespace SGAA.Domain.Core
{
    using SGAA.Domain.Auth;
    using SGAA.Domain.Base;
    using System;

    public class Firma : BaseEntity, IEntity
    {
        public Firma(int contratoId, int usuarioId, DateTime? fechaFirma, string? direccionIp, FirmaRol rol, TipoIdentificacion tipoIdentificacion, string numeroIdentificacion, string domicilioCompleto)
        {
            ContratoId = contratoId;
            UsuarioId = usuarioId;
            FechaFirma = fechaFirma;
            DireccionIp = direccionIp;
            Rol = rol;
            TipoIdentificacion = tipoIdentificacion;
            NumeroIdentificacion = numeroIdentificacion;
            DomicilioCompleto = domicilioCompleto;
        }

        public int ContratoId { get; private set; }
        public int UsuarioId { get; set; }
        public DateTime? FechaFirma { get; private set; }
        public string? DireccionIp { get; private set; }
        public FirmaRol Rol { get; private set; }
        public TipoIdentificacion TipoIdentificacion { get; private set; }
        public string NumeroIdentificacion { get; private set; }
        public string DomicilioCompleto { get; private set; }

        public Contrato Contrato { get; private set; } = default!;
        public Usuario Usuario { get; set; } = default!;
    }
}
