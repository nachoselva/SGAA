namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Contrato : BaseEntity, IEntity
    {
        public Contrato(DateOnly fechaDesde, DateOnly fechaHasta, DateTimeOffset? fechaCancelacion, decimal montoAlquiler, int ordenRenovacion)
        {
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            FechaCancelacion = fechaCancelacion;
            MontoAlquiler = montoAlquiler;
            OrdenRenovacion = ordenRenovacion;
        }

        public DateOnly FechaDesde { get; private set; }
        public DateOnly FechaHasta { get; private set; }
        public DateTimeOffset? FechaCancelacion { get; private set; }
        public decimal MontoAlquiler { get; private set; }
        public int OrdenRenovacion { get; private set; }

        public Postulacion Postulacion { get; private set; } = default!;
        public IReadOnlyCollection<Firma> Ciudades { get; private set; } = Array.Empty<Firma>();
        public IReadOnlyCollection<Pago> Pagos { get; private set; } = Array.Empty<Pago>();
    }
}
