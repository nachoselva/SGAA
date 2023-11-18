namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Postulacion : BaseEntity, IEntity
    {
        public Postulacion(int publicacionId, int aplicacionId, PostulacionStatus status, DateTime? fechaOferta)
        {
            PublicacionId = publicacionId;
            AplicacionId = aplicacionId;
            Status = status;
            FechaOferta = fechaOferta;
        }

        public int PublicacionId { get; private set; }
        public int AplicacionId { get; private set; }
        public PostulacionStatus Status { get; set; }
        public DateTime? FechaOferta { get; set; }

        public Publicacion Publicacion { get; private set; } = default!;
        public Aplicacion Aplicacion { get; private set; } = default!;
        public IReadOnlyCollection<Contrato> Contratos { get; set; } = default!;
    }
}
