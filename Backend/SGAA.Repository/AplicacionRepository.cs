﻿namespace SGAA.Repository
{
    using Microsoft.EntityFrameworkCore;
    using SGAA.Domain.Core;
    using SGAA.Repository.Contexts;
    using SGAA.Repository.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AplicacionRepository : IAplicacionRepository
    {
        private readonly SGAADbContext _dbContext;
        public AplicacionRepository(SGAADbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<Aplicacion> AplicacionQuery()
        =>
            _dbContext.Aplicaciones
            .Include(a => a.Comentarios)
            .Include(a => a.Garantias)
            .Include(a => a.Postulantes)
            .Include(a => a.InquilinoUsuario)
            .Include(a => a.Postulaciones)
            .OrderByDescending(a => a.Audit.CreatedOn);

        public Task<Aplicacion?> GetAplicacion(int aplicacionId)
        => AplicacionQuery()
            .Where(ap => ap.Id == aplicacionId)
            .FirstOrDefaultAsync();

        public async Task<IReadOnlyCollection<Aplicacion>> GetAplicaciones(int usuarioId)
        => await AplicacionQuery()
                .Where(ap => ap.InquilinoUsuarioId == usuarioId)
                .ToListAsync();

        public async Task<IReadOnlyCollection<Aplicacion>> GetAplicaciones()
         => await AplicacionQuery()
            .ToListAsync();

        public async Task<Aplicacion> AddAplicacion(Aplicacion aplicacion)
        {
            var entityEntry = await _dbContext.Aplicaciones.AddAsync(aplicacion);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<Aplicacion> UpdateAplicacion(Aplicacion aplicacion)
        {
            var entityEntry = _dbContext.Aplicaciones.Update(aplicacion);
            await _dbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task DeleteGarantias(IEnumerable<Garantia> entitiesToDelete)
        {
            IEnumerable<int> idsToDelete = entitiesToDelete.Select(e => e.Id);
            _dbContext.Garantias.RemoveRange(_dbContext.Garantias.Where(img => idsToDelete.Contains(img.Id)));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePostulantes(IEnumerable<Postulante> entitiesToDelete)
        {
            IEnumerable<int> idsToDelete = entitiesToDelete.Select(e => e.Id);
            _dbContext.Postulantes.RemoveRange(_dbContext.Postulantes.Where(img => idsToDelete.Contains(img.Id)));
            await _dbContext.SaveChangesAsync();
        }

        public Task<IndiceValor?> GetIcl(DateOnly today)
        {
            return _dbContext.Valores
                .Where(v => v.Indice.Nombre == IndiceTipo.ICL)
                .Where(v => v.FechaDesde <= today)
                .OrderByDescending(v => v.FechaDesde)
                .FirstOrDefaultAsync();
        }
    }
}
