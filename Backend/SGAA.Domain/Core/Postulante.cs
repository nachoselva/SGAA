namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Postulante : Persona, IEntity
    {

        public Postulante(string nombre, string apellido, TipoIdentificacion tipoIdentificacion, string numeroIdentificacion, DateTime fechaNacimiento, string domicilio, byte[] frenteIdentificacionArchivo, byte[] dorsoIdentificacionArchivo, int aplicacionId, DateTime fechaEmpleadoDesde, string nombreEmpresa, decimal ingresoMensual, byte[] reciboDeSueldoArchivo) : base(nombre, apellido, tipoIdentificacion, numeroIdentificacion, fechaNacimiento, domicilio, frenteIdentificacionArchivo, dorsoIdentificacionArchivo)
        {
            AplicacionId = aplicacionId;
            FechaEmpleadoDesde = fechaEmpleadoDesde;
            NombreEmpresa = nombreEmpresa;
            IngresoMensual = ingresoMensual;
            ReciboDeSueldoArchivo = reciboDeSueldoArchivo;
        }

        public int AplicacionId { get; private set; }
        public DateTime FechaEmpleadoDesde { get; private set; }
        public string NombreEmpresa { get; private set; }
        public decimal IngresoMensual { get; private set; }
        public byte[] ReciboDeSueldoArchivo { get; private set; }

        public Aplicacion Aplicacion { get; private set; } = default!;
    }
}
