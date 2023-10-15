namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class PublicacionCerrarPutModel : IPutModel<Publicacion>, IPutModel<Postulacion>
    {
        [JsonIgnore]
        public int? PropietarioUsuarioId { get; set; }
    }
}
