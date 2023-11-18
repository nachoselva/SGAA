namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class RechazarAplicacionPutModel : IPutModel<Aplicacion>
    {
        public required string Comentario { get; set; }
    }
}
