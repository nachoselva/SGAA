namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class PostulacionGetModel : IGetModel<Postulacion>
    {
        public required int Id { get; set; }
        public required int PublicacionId { get; set; }
        public required int AplicacionId { get; set; }
        public required PostulacionStatus Status { get; set; }
        public required DateTime? FechaPostulacion { get; set; }
        public required DateTime? FechaOferta { get; set; }
        public required decimal MontoAlquiler { get; set; }
        public required string DomicilioCompleto { get; set; }
        public required bool CanContratoBeCreated { get; set; }

    }
}
