namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PagoRepository : IPagoRepository
    {
        private readonly SGAADbContext _dbContext;

        public PagoRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<Pago> PagosQuery()
        {
            return _dbContext.Pagos
                .Include(p => p.Contrato)
                .ThenInclude(c => c.Firmas)
                .OrderByDescending(a => a.Audit.CreatedOn);
        }

        public async Task<Pago> AddPago(Pago pago)
        {
            var entityEntry = await _dbContext.Pagos.AddAsync(pago);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public Task<Pago?> GetPago(int pagoId)
        {
            return PagosQuery()
                .Where(p => p.Id == pagoId)
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Pago>> GetPagos(int contratoId)
        {
            return await PagosQuery()
                .Where(p => p.ContratoId == contratoId)
                .ToListAsync();
        }

        public async Task<Pago> UpdatePago(Pago pago)
        {
            var entityEntry = _dbContext.Pagos.Update(pago);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<IReadOnlyCollection<Pago>> AddPagos(IEnumerable<Pago> pagos)
        {
            await _dbContext.Pagos.AddRangeAsync(pagos);
            await _dbContext.SaveChangesAsync();
            return pagos.ToList();
        }
    }
}
