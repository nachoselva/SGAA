namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;

    public class Contrato : BaseEntity, IEntity
    {
        private readonly List<Firma> _firmas;
        private readonly List<Pago> _pagos;

        public Contrato(int postulacionId, DateOnly fechaDesde, DateOnly fechaHasta, DateOnly? fechaCancelacion, decimal montoAlquiler, int ordenRenovacion, byte[] archivo, ContratoStatus status)
        {
            PostulacionId = postulacionId;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            FechaCancelacion = fechaCancelacion;
            MontoAlquiler = montoAlquiler;
            OrdenRenovacion = ordenRenovacion;
            Archivo = archivo;
            Status = status;
            _firmas = new List<Firma>();
            _pagos = new List<Pago>();
        }

        public int PostulacionId { get; private set; }
        public DateOnly FechaDesde { get; private set; }
        public DateOnly FechaHasta { get; private set; }
        public DateOnly? FechaCancelacion { get; set; }
        public decimal MontoAlquiler { get; private set; }
        public int OrdenRenovacion { get; private set; }
        public ContratoStatus Status { get; set; }
        public byte[] Archivo { get; set; }

        public Postulacion Postulacion { get; set; } = default!;
        public IReadOnlyCollection<Firma> Firmas => _firmas;
        public IReadOnlyCollection<Pago> Pagos { get; private set; } = new List<Pago>();

        public void AddFirmas(IEnumerable<Firma> firmas)
        {
            _firmas.AddRange(firmas);
        }

        public void AddPagos(IEnumerable<Pago> pagos)
        {
            _pagos.AddRange(pagos);
        }
    }
}
