namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class AplicacionGetModel : AplicacionBaseModel, IGetModel<Aplicacion>
    {
        public required int Id { get; set; }
        public required AplicacionStatus Status { get; set; }

        public ICollection<ComentarioModel> Comentarios { get; set; } = default!;
    }
}
