namespace SGAA.Api.Controllers.Propietario
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Middleware;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Propietario)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Propietario))]
    public class UnidadController : ControllerBase
    {
        private readonly IUnidadService _unidadService;
        private readonly IUsuarioProvider _usuarioProvider;

        public UnidadController(IUnidadService unidadService, IUsuarioProvider usuarioProvider)
        {
            _unidadService = unidadService;
            _usuarioProvider = usuarioProvider;
        }

        [HttpPost]
        [Transactional]
        public async Task<UnidadGetModel> AddUnidad([FromBody] UnidadPostModel model)
        {
            model.PropietarioUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            UnidadGetModel unidad = await _unidadService.AddUnidad(model);
            return unidad;
        }

        [HttpPut]
        [Route("{unidadId}")]
        [Transactional]
        public async Task<UnidadGetModel> UpdateUnidad([FromRoute] int unidadId, [FromBody] UnidadPutModel model)
        {
            model.PropietarioUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            return await _unidadService.UpdateUnidad(unidadId, model);
        }

        [HttpGet]
        [Route("{unidadId}")]
        public async Task<UnidadGetModel> GetUnidad([FromRoute] int unidadId)
            => await _unidadService.GetUnidad(unidadId);

        [HttpGet]
        public async Task<IReadOnlyCollection<UnidadGetModel>> GetUnidades()
            => await _unidadService.GetUnidades((await _usuarioProvider.GetUser())!.Id);
    }
}
