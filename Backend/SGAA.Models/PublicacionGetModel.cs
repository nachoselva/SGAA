namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class PublicacionGetModel : IGetModel<Publicacion>
    {
        public required int Id { get; set; }
        public required int UnidadId { get; set; }
        public required PublicacionStatus Status { get; set; }
        public required decimal MontoAlquiler { get; set; }
        public required DateOnly InicioAlquiler { get; set; }
        public required string Codigo { get; set; }
        public required string DomicilioCompleto { get; set; }
        public required int? Postulaciones { get; set; }
        public required UnidadGetModel Unidad { get; set; }
        public bool? CanUsuarioPostular { get; set; }
    }
}
