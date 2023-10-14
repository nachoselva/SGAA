namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AplicacionRepository : IAplicacionRepository
    {
        private readonly SGAADbContext _dbContext;
        public AplicacionRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Aplicacion>> GetAplicacionesByInquilinoUsuarioId(int usuarioId)
        {
            return await _dbContext.Aplicaciones
                .Where(ap => ap.InquilinoUsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}
