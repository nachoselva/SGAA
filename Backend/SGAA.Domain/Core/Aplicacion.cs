namespace SGAA.Domain.Core
{
    using SGAA.Domain.Auth;
    using SGAA.Domain.Base;
    using System.Collections.Generic;

    public class Aplicacion : BaseEntity, IEntity
    {
        public Aplicacion(int inquilinoUsuarioId, AplicacionStatus status, decimal puntuacionTotal)
        {
            InquilinoUsuarioId = inquilinoUsuarioId;
            Status = status;
            PuntuacionTotal = puntuacionTotal;
        }

        public int InquilinoUsuarioId { get; private set; }
        public AplicacionStatus Status { get; private set; }
        public decimal PuntuacionTotal { get; private set; }

        public Usuario InquilinoUsuario { get; } = null!;
        public IReadOnlyCollection<Postulacion> Postulaciones { get; } = new List<Postulacion>();
        public IReadOnlyCollection<Postulante> Postulantes { get; } = new List<Postulante>();
        public IReadOnlyCollection<Garantia> Garantias { get; } = new List<Garantia>();
        public IReadOnlyCollection<AplicacionComentario> Comentarios { get; } = new List<AplicacionComentario>();
    }
}
