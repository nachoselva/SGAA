﻿namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using System.Collections.Generic;

    public class CiudadRepository : ICiudadRepository
    {
        private readonly SGAADbContext _dbContext;

        public CiudadRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Ciudad>> GetAllCiudades(int provinciaId)
        {
            return await _dbContext.Ciudades.Where(c => c.ProvinciaId == provinciaId).ToListAsync();
        }
    }
}
