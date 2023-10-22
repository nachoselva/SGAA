namespace SGAA.Repository
{
    using Contexts;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using System.Collections.Generic;

    public class ProvinciaRepository : IProvinciaRepository
    {
        private readonly SGAADbContext _dbContext;

        public ProvinciaRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Provincia>> GetProvincias()
        {
            return await _dbContext.Provincias.ToListAsync();
        }
    }
}
