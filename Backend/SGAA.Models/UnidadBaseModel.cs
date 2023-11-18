namespace SGAA.Models
{
    using System.Text.Json.Serialization;

    public abstract class UnidadBaseModel
    {
        [JsonIgnore]
        public int? PropiedadId { get; set; }
        [JsonIgnore]
        public int? PropietarioUsuarioId { get; set; }

        public required int CiudadId { get; set; }
        public required int ProvinciaId { get; set; }
        public required string Calle { get; set; }
        public required int Altura { get; set; }
        public required string Piso { get; set; }
        public required string Departamento { get; set; }
        public required DateTime FechaAdquisicion { get; set; }
        public required string TituloPropiedadArchivo { get; set; }

        public required UnidadDetalleModel Detalle { get; set; }
        public ICollection<TitularModel> Titulares { get; set; } = default!;
    }
}
