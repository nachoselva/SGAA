namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class PagoGetModel : IGetModel<Pago>
    {
        public required int ContratoId { get; set; }
        public required string Descripcion { get; set; }
        public required decimal Monto { get; set; }
        public required DateOnly FechaVencimiento { get; set; }
        public required PagoStatus Status { get; set; }
        public required DateTime? FechaPago { get; set; }
        public required string? Archivo { get; set; }
    }
}
