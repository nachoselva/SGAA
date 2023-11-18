namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class AbonarPagoPutModel : IPutModel<Pago>
    {
        [JsonIgnore]
        public int? InquilinoUsuarioId { get; set; }
        public required string Archivo { get; set; }
    }
}
