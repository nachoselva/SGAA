﻿namespace SGAA.Domain.Core
{
    using SGAA.Domain.Auth;
    using SGAA.Domain.Base;
    using System;

    public class Firma : BaseEntity, IEntity
    {
        public Firma(int contratoId, int usuarioId, DateTime? fechaFirma, string? direccionIp, FirmaRol rol, string numeroIdentificacion, string domicilio)
        {
            ContratoId = contratoId;
            UsuarioId = usuarioId;
            FechaFirma = fechaFirma;
            DireccionIp = direccionIp;
            Rol = rol;
            NumeroIdentificacion = numeroIdentificacion;
            Domicilio = domicilio;
        }

        public int ContratoId { get; private set; }
        public int UsuarioId { get; set; }
        public DateTime? FechaFirma { get; set; }
        public string? DireccionIp { get; set; }
        public FirmaRol Rol { get; private set; }
        public string NumeroIdentificacion { get; private set; }
        public string Domicilio { get; private set; }

        public Contrato Contrato { get; private set; } = default!;
        public Usuario Usuario { get; set; } = default!;
    }
}
