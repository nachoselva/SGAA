namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using System.Collections.Generic;

    public class ProvinciaRepository : IProvinciaRepository
    {
        private readonly SGAADbContext _dbContext;

        public ProvinciaRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Provincia>> GetAllProvincias()
        {
            return await _dbContext.Provincias.ToListAsync();
        }
    }
}
