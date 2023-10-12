namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;
    using System.Text.Json.Serialization;

    public class UnidadPostModel : IPostModel<Unidad>
    {
        public required int CiudadId { get; set; }
        public required string Calle { get; set; }
        public required int Altura { get; set; }

        [JsonIgnore]
        public int? PropiedadId { get; set; }
        public required int PropietarioUsuarioId { get; set; }
        public required string Piso { get; set; }
        public required string Departamento { get; set; }
        public required DateTime FechaAdquisicion { get; set; }
        public required string TituloPropiedadArchivo { get; set; }

        public required string Descripcion { get; set; }
        public required decimal Superficie { get; set; }
        public required int Ambientes { get; set; }
        public required int Banios { get; set; }
        public required int Dormitorios { get; set; }
        public required int Cocheras { get; set; }

        public required ICollection<UnidadImagenModel> Imagenes { get; set; }
    }

    public class UnidadImagenModel
    {
        public required int UnidadDetalleId { get; set; }
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required string Archivo { get; set; }
    }
}
