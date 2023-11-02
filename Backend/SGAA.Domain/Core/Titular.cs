namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Titular : Persona, IEntity
    {
        public Titular(int unidadId, string nombre, string apellido, string email, string numeroIdentificacion, DateTime fechaNacimiento, string domicilio, string frenteIdentificacionArchivo, string dorsoIdentificacionArchivo) 
            : base(nombre, apellido, email, numeroIdentificacion, fechaNacimiento, domicilio, frenteIdentificacionArchivo, dorsoIdentificacionArchivo)
        {
            UnidadId = unidadId;
        }

        public int UnidadId { get; set; }

        public Unidad Unidad { get; private set; } = default!;
    }
}
