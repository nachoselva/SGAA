namespace SGAA.Repository
{
    using Contexts;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using System.Collections.Generic;

    public class CiudadRepository : ICiudadRepository
    {
        private readonly SGAADbContext _dbContext;

        public CiudadRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Ciudad>> GetCiudades(int provinciaId)
        {
            return await _dbContext.Ciudades.Where(c => c.ProvinciaId == provinciaId).ToListAsync();
        }
    }
}
