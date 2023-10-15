namespace SGAA.Models
{
    using System.Text.Json.Serialization;

    public class AplicacionBaseModel
    {
        [JsonIgnore]
        public int? InquilinoUsuarioId { get; set; }
        [JsonIgnore]
        public decimal? PuntuacionTotal { get; set; }


        public ICollection<PostulanteModel> Postulantes { get; set; } = default!;
        public ICollection<GarantiaModel> Garantias { get; set; } = default!;
    }
}
