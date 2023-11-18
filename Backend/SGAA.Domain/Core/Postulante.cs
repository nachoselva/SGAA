namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Postulante : Persona, IEntity
    {
        public Postulante(string nombre, string apellido, string email, string numeroIdentificacion,
            DateTime fechaNacimiento, string domicilio, string frenteIdentificacionArchivo, string dorsoIdentificacionArchivo,
            int aplicacionId, DateTime fechaEmpleadoDesde, string nombreEmpresa, decimal ingresoMensual, string reciboDeSueldoArchivo,
            int? puntuacionCrediticia, int? puntuacionPenal)
            : base(nombre, apellido, email, numeroIdentificacion, fechaNacimiento, domicilio, frenteIdentificacionArchivo, dorsoIdentificacionArchivo)
        {
            AplicacionId = aplicacionId;
            FechaEmpleadoDesde = fechaEmpleadoDesde;
            NombreEmpresa = nombreEmpresa;
            IngresoMensual = ingresoMensual;
            ReciboDeSueldoArchivo = reciboDeSueldoArchivo;
            PuntuacionCrediticia = puntuacionCrediticia;
            PuntuacionPenal = puntuacionPenal;
        }

        public int AplicacionId { get; set; }
        public DateTime FechaEmpleadoDesde { get; set; }
        public string NombreEmpresa { get; set; }
        public decimal IngresoMensual { get; set; }
        public string ReciboDeSueldoArchivo { get; set; }
        public int? PuntuacionCrediticia { get; set; }
        public int? PuntuacionPenal { get; set; }

        public Aplicacion Aplicacion { get; private set; } = default!;
    }
}
