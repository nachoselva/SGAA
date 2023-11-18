namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Core;

    public interface IPagoRepository
    {
        public Task<Pago?> GetPago(int pagoId);
        public Task<IReadOnlyCollection<Pago>> GetPagos(int contratoId);
        public Task<IReadOnlyCollection<Pago>> GetPagos();
        public Task<Pago> UpdatePago(Pago pago);
        public Task<Pago> AddPago(Pago pago);
        public Task<IReadOnlyCollection<Pago>> AddPagos(IEnumerable<Pago> pago);
    }
}
