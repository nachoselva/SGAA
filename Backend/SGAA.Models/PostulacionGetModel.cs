namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class PostulacionGetModel : IGetModel<Postulacion>
    {
        public int Id { get; set; }
        public int PublicacionId { get; set; }
        public int AplicacionId { get; set; }
        public PostulacionStatus Status { get; set; }
        public DateTime? FechaPostulacion { get; set; }
        public DateTime? FechaOferta { get; set; }
        public decimal MontoAlquiler { get; set; }
        public required string DomicilioCompleto { get; set; }

    }
}
