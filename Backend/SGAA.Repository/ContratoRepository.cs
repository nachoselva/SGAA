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
            .ThenInclude(ci => ci.Provincia)
            .Include(c => c.Postulacion)
            .ThenInclude(p => p.Aplicacion)
            .ThenInclude(a => a.Postulantes)
            .Include(c => c.Postulacion)
            .ThenInclude(p => p.Publicacion)
            .ThenInclude(pu => pu.Unidad)
            .ThenInclude(u => u.Titulares)
            .OrderByDescending(a => a.Audit.CreatedOn);

        public async Task<IReadOnlyCollection<Contrato>> GetContratos()
        {
            return await ContratosQuery()
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Contrato>> GetContratos(int usuarioId)
        {
            return await ContratosQuery()
                .Where(c => c.Firmas.Any(f => f.UsuarioId == usuarioId))
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Contrato>> GetContratosByRol(int usuarioId, FirmaRol rol)
        {
            return await ContratosQuery()
                .Where(c => c.Firmas.Any(f => f.UsuarioId == usuarioId && f.Rol == rol))
                .ToListAsync();
        }

        public async Task<Contrato> UpdateContrato(Contrato contrato)
        {
            var entityEntry = _dbContext.Contratos.Update(contrato);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Contrato> AddContrato(Contrato contrato)
        {
            _dbContext.Usuarios.UpdateRange(contrato.Firmas.Select(f => f.Usuario));
            var entityEntry = await _dbContext.Contratos.AddAsync(contrato);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public Task<Contrato?> GetContrato(int contratoId)
        {
            return ContratosQuery()
                .Where(c => c.Id == contratoId)
                .FirstOrDefaultAsync();
        }
    }
}
