namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public abstract class Persona : BaseEntity, IEntity
    {
        protected Persona(string nombre, string apellido, string email, TipoIdentificacion tipoIdentificacion, string numeroIdentificacion, DateTime fechaNacimiento, string domicilio, byte[] frenteIdentificacionArchivo, byte[] dorsoIdentificacionArchivo)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            TipoIdentificacion = tipoIdentificacion;
            NumeroIdentificacion = numeroIdentificacion;
            FechaNacimiento = fechaNacimiento;
            Domicilio = domicilio;
            FrenteIdentificacionArchivo = frenteIdentificacionArchivo;
            DorsoIdentificacionArchivo = dorsoIdentificacionArchivo;
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public byte[] FrenteIdentificacionArchivo { get; set; }
        public byte[] DorsoIdentificacionArchivo { get; set; }
        public string Email { get; set; }
    }
}
