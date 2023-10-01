namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class AplicacionComentario : BaseEntity, IEntity
    {
        public AplicacionComentario(int aplicacionId, string comentario, DateTime fecha)
        {
            AplicacionId = aplicacionId;
            Comentario = comentario;
            Fecha = fecha;
        }

        public int AplicacionId { get; private set; }
        public string Comentario { get; private set; }
        public DateTime Fecha { get; private set; }
        public Aplicacion Aplicacion { get; } = default!;
    }
}
