﻿namespace SGAA.Service
{
    using Contracts;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using System.Collections.Generic;

    public class CiudadService : ICiudadService
    {
        private readonly ICiudadRepository _ciudadRepository;
        private readonly ICiudadMapper _ciudadMapper;

        public CiudadService(ICiudadRepository ciudadRepository, ICiudadMapper ciudadMapper)
        {
            _ciudadRepository = ciudadRepository;
            _ciudadMapper = ciudadMapper;
        }

        public async Task<IReadOnlyCollection<CiudadGetModel>> GetCiudades(int provinciaId)
        {
            return (await _ciudadRepository.GetCiudades(provinciaId))
                .Select(p => p.MapToGetModel(_ciudadMapper))
                .ToList();
        }
    }
}
