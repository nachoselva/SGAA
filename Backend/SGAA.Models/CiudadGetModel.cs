namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class CiudadGetModel : IGetModel<Ciudad>
    {
        public required int Id { get; set; }
        public required int ProvinciaId { get; set; }
        public required string Nombre { get; set; }
        public required string NombreCompleto { get; set; }
    }
}
