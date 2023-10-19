namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Titular : Persona, IEntity
    {
        public Titular(int unidadId, string nombre, string apellido, string email, TipoIdentificacion tipoIdentificacion, string numeroIdentificacion, DateTime fechaNacimiento, string domicilio, byte[] frenteIdentificacionArchivo, byte[] dorsoIdentificacionArchivo) 
            : base(nombre, apellido, email, tipoIdentificacion, numeroIdentificacion, fechaNacimiento, domicilio, frenteIdentificacionArchivo, dorsoIdentificacionArchivo)
        {
            UnidadId = unidadId;
        }

        public int UnidadId { get; set; }

        public Unidad Unidad { get; private set; } = default!;
    }
}
