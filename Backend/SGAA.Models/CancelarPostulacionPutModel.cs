namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class CancelarPostulacionPutModel : IPutModel<Postulacion>, IPutModel<Publicacion>, IPutModel<Aplicacion>
    {
        [JsonIgnore]
        public int? InquilinoUsuarioId { get; set; }
    }
}
