namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class ComentarioModel : IGetModel<AplicacionComentario>, IGetModel<UnidadComentario>
    {
        public required DateTime Fecha { get; set; }
        public required string Comentario { get; set; }
    }
}
