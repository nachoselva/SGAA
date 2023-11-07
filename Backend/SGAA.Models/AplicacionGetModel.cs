namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class AplicacionGetModel : AplicacionBaseModel, IGetModel<Aplicacion>
    {
        public required int Id { get; set; }
        public required AplicacionStatus Status { get; set; }
        public required bool IsActive { get; set; }
        public required int? Postulaciones { get; set; }
        public required string InquilinoUsuarioNombreCompleto { get; set; }
        public required decimal? PuntuacionTotal { get; set; }

        public ICollection<ComentarioModel> Comentarios { get; set; } = default!;
    }
}
