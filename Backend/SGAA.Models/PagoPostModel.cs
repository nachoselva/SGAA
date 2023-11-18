namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class PagoPostModel : IPostModel<Pago>
    {
        public required int ContratoId { get; set; }
        [JsonIgnore]
        public int? PropietarioUsuarioId { get; set; }
        public required string Descripcion { get; set; }
        public required decimal Monto { get; set; }
        public required DateTime FechaVencimiento { get; set; }
    }
}
