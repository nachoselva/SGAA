namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class PostulanteModel : IGetModel<Postulante>, IPostModel<Postulante>, IPutModel<Postulante>
    {
        public int? Id { get; set; }
        public int? AplicacionId { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string NumeroIdentificacion { get; set; }
        public required DateTime FechaNacimiento { get; set; }
        public required string Domicilio { get; set; }
        public required string FrenteIdentificacionArchivo { get; set; }
        public required string DorsoIdentificacionArchivo { get; set; }
        public required DateTime FechaEmpleadoDesde { get; set; }
        public required string NombreEmpresa { get; set; }
        public required decimal IngresoMensual { get; set; }
        public required string ReciboDeSueldoArchivo { get; set; }
        public decimal? PuntuacionCrediticia { get; set; }
        public decimal? PuntuacionPenal { get; set; }
    }
}
