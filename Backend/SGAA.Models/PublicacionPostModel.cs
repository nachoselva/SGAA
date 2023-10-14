namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class PublicacionPostModel : IPostModel<Publicacion>
    {
        [JsonIgnore]
        public int? PropietarioUsuarioId { get; set; }
        public required int UnidadId { get; set; }
        public required decimal MontoAlquiler { get; set; }
        public required DateOnly InicioAlquiler { get; set; }
        [JsonIgnore]
        public string? Codigo { get; set; }
    }
}
