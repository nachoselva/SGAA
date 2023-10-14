namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class PublicacionCancelPutModel : IPutModel<Publicacion>
    {
        [JsonIgnore]
        public int? PropietarioUsuarioId { get; set; }
    }
}
