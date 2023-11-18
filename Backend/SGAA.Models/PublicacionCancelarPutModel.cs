namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class PublicacionCancelarPutModel : IPutModel<Publicacion>, IPutModel<Postulacion>
    {
        [JsonIgnore]
        public int? PropietarioUsuarioId { get; set; }
    }
}
