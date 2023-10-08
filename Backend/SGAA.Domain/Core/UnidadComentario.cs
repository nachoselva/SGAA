namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class UnidadComentario : BaseEntity, IEntity
    {
        public UnidadComentario(int unidadId, string comentario, DateTime fecha)
        {
            UnidadId = unidadId;
            Comentario = comentario;
            Fecha = fecha;
        }

        public int UnidadId { get; private set; }
        public string Comentario { get; private set; }
        public DateTime Fecha { get; private set; }

        public Unidad Unidad { get; private set; } = default!;
    }
}
