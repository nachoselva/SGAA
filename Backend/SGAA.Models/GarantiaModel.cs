namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class GarantiaModel : IGetModel<Garantia>, IPostModel<Garantia>, IPutModel<Garantia>
    {
        public int? Id { get; set; }
        public int? AplicacionId { get; set; }
        public required decimal Monto { get; set; }
        public required string Archivo { get; set; }

    }
}
