namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Publicacion : BaseEntity, IEntity
    {
        public Publicacion(int unidadId, decimal montoAlquiler, DateOnly inicioAlquiler, string codigo, PublicacionStatus status)
        {
            UnidadId = unidadId;
            MontoAlquiler = montoAlquiler;
            InicioAlquiler = inicioAlquiler;
            Codigo = codigo;
            Status = status;
        }

        public int UnidadId { get; private set; }
        public decimal MontoAlquiler { get; private set; }
        public DateOnly InicioAlquiler { get; private set; }
        public string Codigo { get; private set; }
        public PublicacionStatus Status { get; set; }

        public Unidad Unidad { get; private set; } = default!;
        public IReadOnlyCollection<Postulacion> Postulaciones { get; private set; } = new List<Postulacion>();   
    }
}
