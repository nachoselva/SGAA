namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class PagoPostModel : IPostModel<Pago>
    {
        public required int ContratoId { get; set; }
        public required int? PropietarioUsuarioId { get; set; }
        public required string Descripcion { get; set; }
        public required decimal Monto { get; set; }
        public required DateOnly FechaVencimiento { get; set; }
    }
}
