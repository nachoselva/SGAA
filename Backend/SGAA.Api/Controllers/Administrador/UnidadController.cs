namespace SGAA.Api.Controllers.Administrador
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Middleware;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Administrador)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Administrador))]
    public class UnidadController : ControllerBase
    {
        private readonly IUnidadService _unidadService;

        public UnidadController(IUnidadService unidadService)
        {
            _unidadService = unidadService;
        }

        [HttpGet]
        [Route("{unidadId}")]
        public async Task<UnidadGetModel> GetUnidad([FromRoute] int unidadId)
            => await _unidadService.GetUnidad(unidadId);

        [HttpGet]
        public async Task<IReadOnlyCollection<UnidadGetModel>> GetUnidadesAdmin()
            => await _unidadService.GetUnidades();

        [HttpPut]
        [Route("{unidadId}/aprobar")]
        [Transactional]
        public async Task<ActionResult<UnidadGetModel>> AprobarUnidad([FromRoute] int unidadId, [FromBody] AprobarUnidadPutModel model)
            => await _unidadService.AprobarUnidad(unidadId, model);

        [HttpPut]
        [Route("{unidadId}/rechazar")]
        [Transactional]
        public async Task<ActionResult<UnidadGetModel>> RechazarUnidad([FromRoute] int unidadId, [FromBody] RechazarUnidadPutModel model)
            => await _unidadService.RechazarUnidad(unidadId, model);
    }
}
