namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class AprobarPagoPutModel : IPutModel<Pago>
    {
        public required int? PropietarioUsuarioId { get; set; }
    }
}
