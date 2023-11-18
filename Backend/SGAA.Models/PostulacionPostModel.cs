namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class PostulacionPostModel : IPostModel<Postulacion>
    {
        [JsonIgnore]
        public int? InquilinoUsuarioId { get; set; }
        public required int PublicacionId { get; set; }
        [JsonIgnore]
        public int? AplicacionId { get; set; }
    }
}
