namespace SGAA.Service
{
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository;
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
            return (await _ciudadRepository.GetAllCiudades(provinciaId))
                .Select(p => p.MapToGetModel(_ciudadMapper))
                .ToList();
        }
    }
}
