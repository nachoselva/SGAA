namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class AbonarPagoPutModel : IPutModel<Pago>
    {
        public required int? InquilinoUsuarioId { get; set; }
        public required string Archivo { get; set; }
    }
}
