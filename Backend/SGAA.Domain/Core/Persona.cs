namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public abstract class Persona : BaseEntity, IEntity
    {
        protected Persona(string nombre, string apellido, string email, string numeroIdentificacion, DateTime fechaNacimiento, string domicilio, string frenteIdentificacionArchivo, string dorsoIdentificacionArchivo)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            NumeroIdentificacion = numeroIdentificacion;
            FechaNacimiento = fechaNacimiento;
            Domicilio = domicilio;
            FrenteIdentificacionArchivo = frenteIdentificacionArchivo;
            DorsoIdentificacionArchivo = dorsoIdentificacionArchivo;
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroIdentificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public string FrenteIdentificacionArchivo { get; set; }
        public string DorsoIdentificacionArchivo { get; set; }
        public string Email { get; set; }
    }
}
