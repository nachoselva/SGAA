namespace SGAA.Service
{
    using Contracts;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using System.Collections.Generic;

    public class ProvinciaService : IProvinciaService
    {
        private readonly IProvinciaRepository _provinciaRepository;
        private readonly IProvinciaMapper _provinciaMapper;

        public ProvinciaService(IProvinciaRepository provinciaRepository, IProvinciaMapper provinciaMapper)
        {
            _provinciaRepository = provinciaRepository;
            _provinciaMapper = provinciaMapper;
        }

        public async Task<IReadOnlyCollection<ProvinciaGetModel>> GetProvincias()
        {
            return (await _provinciaRepository.GetProvincias())
                .Select(p => p.MapToGetModel(_provinciaMapper))
                .ToList();
        }
    }
}
