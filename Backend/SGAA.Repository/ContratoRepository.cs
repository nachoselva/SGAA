namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;

    public class ContratoRepository : IContratoRepository
    {
        private readonly SGAADbContext _dbContext;

        public ContratoRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<Contrato> ContratosQuery()
       => _dbContext.Contratos
                .Include(c => c.Firmas)
                .ThenInclude(f => f.Usuario)
                .Include(c => c.Postulacion)
                .ThenInclude(p => p.Publicacion)
                .ThenInclude(pu => pu.Unidad)
                .ThenInclude(u => u.Propiedad)
                .ThenInclude(p => p.Ciudad)
                .ThenInclude(ci => ci.Provincia);

        public async Task<IReadOnlyCollection<Contrato>> GetContratosAdmin()
        {
            return await ContratosQuery()
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Contrato>> GetContratosByUsuarioId(int usuarioId)
        {
            return await ContratosQuery()
                .Where(c => c.Firmas.Any(f => f.UsuarioId == usuarioId))
                .ToListAsync();
        }

        public async Task<Contrato> UpdateContrato(Contrato contrato)
        { 
            _dbContext.Contratos.Update(contrato);
            await _dbContext.SaveChangesAsync();
            return contrato;
        }

        public async Task<Contrato> AddContrato(Contrato contrato)
        {
            _dbContext.Usuarios.UpdateRange(contrato.Firmas.Select(f => f.Usuario));
            var entityEntry = await _dbContext.Contratos.AddAsync(contrato);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}
