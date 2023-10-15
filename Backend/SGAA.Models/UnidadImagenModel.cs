namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class UnidadImagenModel : IGetModel<UnidadImagen>, IPostModel<UnidadImagen>, IPutModel<UnidadImagen>
    {
        public int? Id { get; set; }
        public int? UnidadDetalleId { get; set; }
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required string Archivo { get; set; }
    }
}
