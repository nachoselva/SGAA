namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class UnidadGetModel : UnidadBaseModel, IGetModel<Unidad>
    {
        public required int Id { get; set; }
        public required UnidadStatus Status { get; set; }
        public required string Provincia { get; set; }
        public required string Ciudad { get; set; }
        public required string DomicilioCompleto { get; set; }
        public required bool CanBePublicada { get; set; }

        public ICollection<ComentarioModel> Comentarios { get; set; } = default!;
    }
}
