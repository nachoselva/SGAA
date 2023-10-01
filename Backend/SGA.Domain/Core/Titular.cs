﻿namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Titular : Persona, IEntity
    {
        public Titular(string nombre, string apellido, TipoIdentificacion tipoIdentificacion, string numeroIdentificacion, DateTime fechaNacimiento, string domicilio, byte[] frenteIdentificacionArchivo, byte[] dorsoIdentificacionArchivo, int unidadId) : base(nombre, apellido, tipoIdentificacion, numeroIdentificacion, fechaNacimiento, domicilio, frenteIdentificacionArchivo, dorsoIdentificacionArchivo)
        {
            UnidadId = unidadId;
        }

        public int UnidadId { get; private set; }

        public Unidad Unidad { get; private set; } = default!;
    }
}
