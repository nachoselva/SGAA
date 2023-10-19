namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Contrato : BaseEntity, IEntity
    {
        private readonly List<Firma> _firmas;

        public Contrato(DateOnly fechaDesde, DateOnly fechaHasta, DateOnly? fechaCancelacion, decimal montoAlquiler, int ordenRenovacion, byte[] archivo, ContratoStatus status)
        {
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            FechaCancelacion = fechaCancelacion;
            MontoAlquiler = montoAlquiler;
            OrdenRenovacion = ordenRenovacion;
            Archivo = archivo;
            Status = status;
            _firmas = new List<Firma>();
        }

        public DateOnly FechaDesde { get; private set; }
        public DateOnly FechaHasta { get; private set; }
        public DateOnly? FechaCancelacion { get; private set; }
        public decimal MontoAlquiler { get; private set; }
        public int OrdenRenovacion { get; private set; }
        public ContratoStatus Status { get; set; }
        public byte[] Archivo { get; set; }

        public Postulacion Postulacion { get; set; } = default!;
        public IReadOnlyCollection<Firma> Firmas => _firmas;
        public IReadOnlyCollection<Pago> Pagos { get; private set; } = new List<Pago>();

        public void AddFirmas(List<Firma> firmas)
        {
            _firmas.AddRange(firmas);
        }
    }
}
