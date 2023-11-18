namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class RechazarUnidadPutModel : IPutModel<Unidad>
    {
        public required string Comentario { get; set; }
    }
}
