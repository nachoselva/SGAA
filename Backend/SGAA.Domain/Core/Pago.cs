namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class Pago : BaseEntity, IEntity
    {
        public Pago(int contratoId, string descripcion, decimal monto, DateOnly fechaVencimiento, PagoStatus status, DateTime? fechaPago, string? archivo)
        {
            ContratoId = contratoId;
            Descripcion = descripcion;
            Monto = monto;
            FechaVencimiento = fechaVencimiento;
            Status = status;
            FechaPago = fechaPago;
            Archivo = archivo;
        }

        public int ContratoId { get; private set; }
        public string Descripcion { get; private set; }
        public decimal Monto { get; private set; }
        public DateOnly FechaVencimiento { get; private set; }
        public PagoStatus Status { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? Archivo { get; set; }

        public Contrato Contrato { get; private set; } = default!;
    }
}
