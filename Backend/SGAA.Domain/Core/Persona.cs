namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public abstract class Persona : BaseEntity, IEntity
    {
        protected Persona(string nombre, string apellido, TipoIdentificacion tipoIdentificacion, string numeroIdentificacion, DateTime fechaNacimiento, string domicilio, byte[] frenteIdentificacionArchivo, byte[] dorsoIdentificacionArchivo)
        {
            Nombre = nombre;
            Apellido = apellido;
            TipoIdentificacion = tipoIdentificacion;
            NumeroIdentificacion = numeroIdentificacion;
            FechaNacimiento = fechaNacimiento;
            Domicilio = domicilio;
            FrenteIdentificacionArchivo = frenteIdentificacionArchivo;
            DorsoIdentificacionArchivo = dorsoIdentificacionArchivo;
        }

        public string Nombre { get; protected set; }
        public string Apellido { get; protected set; }
        public TipoIdentificacion TipoIdentificacion { get; protected set; }
        public string NumeroIdentificacion { get; protected set; }
        public DateTime FechaNacimiento { get; protected set; }
        public string Domicilio { get; protected set; }
        public byte[] FrenteIdentificacionArchivo { get; protected set; }
        public byte[] DorsoIdentificacionArchivo { get; protected set; }
    }
}
