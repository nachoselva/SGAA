namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class UnidadDetalleModel : IGetModel<UnidadDetalle>, IPostModel<UnidadDetalle>, IPutModel<UnidadDetalle>
    {
        public int? Id { get; set; }
        public int? UnidadId { get; set; }
        public required string Descripcion { get; set; }
        public required decimal Superficie { get; set; }
        public required int Ambientes { get; set; }
        public required int Banios { get; set; }
        public required int Dormitorios { get; set; }
        public required int Cocheras { get; set; }

        public ICollection<UnidadImagenModel>? Imagenes { get; set; }
    }
}
