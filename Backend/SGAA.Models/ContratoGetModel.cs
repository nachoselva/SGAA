namespace SGAA.Models
{
    using SGAA.Domain.Auth;
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class ContratoGetModel : IGetModel<Contrato>
    {
        public required int Id { get; set; }
        public required int PostulacionId { get; set; }
        public required int AplicacionId { get; set; }
        public required string Archivo { get; set; }
        public required DateOnly FechaDesde { get; set; }
        public required DateOnly FechaHasta { get; set; }
        public required DateOnly? FechaCancelacion { get; set; }
        public required decimal MontoAlquiler { get; set; }
        public required int OrdenRenovacion { get; set; }
        public required ContratoStatus Status { get; set; }
        public required string Domicilio { get; set; }
        public required int InquilinosCount { get; set; }
        public required int PropietariosCount { get; set; }
        public required string Inquilinos { get; set; }
        public required string Propietarios { get; set; }
        public bool CanUsuarioFirmar { get; set; }
    }
}
