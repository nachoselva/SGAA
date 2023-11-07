namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class AprobarPagoPutModel : IPutModel<Pago>
    {
        [JsonIgnore]
        public required int? PropietarioUsuarioId { get; set; }
    }
}
