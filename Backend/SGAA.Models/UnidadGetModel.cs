namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class UnidadGetModel : UnidadBaseModel, IGetModel<Unidad>
    {
        public required int Id { get; set; }
        public required UnidadStatus Status { get; set; }
    }
}
