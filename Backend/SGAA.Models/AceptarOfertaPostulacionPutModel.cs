namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class AceptarOfertaPostulacionPutModel : IPutModel<Postulacion>, IPutModel<Publicacion>, IPutModel<Aplicacion>, IPostModel<Contrato>
    {
        [JsonIgnore]
        public int? InquilinoUsuarioId { get; set; }
        public required DateOnly FechaDesde { get; set; }
        public required DateOnly FechaHasta { get; set; }
    }
}
