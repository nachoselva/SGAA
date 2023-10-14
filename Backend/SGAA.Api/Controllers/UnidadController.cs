namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [Authorize(Roles = nameof(RolType.Propietario))]
        public async Task<ActionResult<UnidadGetModel>> AddUnidad([FromBody] UnidadPostModel model)
        {
            model.PropietarioUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            UnidadGetModel unidad = await _unidadService.AddUnidad(model);
            return CreatedAtAction(nameof(GetUnidad), new { unidadId = unidad.Id }, model);
        }

        [HttpPut]
        [Route("{unidadId}")]
        [Authorize(Roles = nameof(RolType.Propietario))]
        public async Task<ActionResult<UnidadGetModel>> UpdateUnidad([FromRoute] int unidadId, [FromBody] UnidadPutModel model)
        {
            model.PropietarioUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            return await _unidadService.UpdateUnidad(unidadId, model);
        }

        [HttpGet]
        [Route("{unidadId}")]
        [Authorize(Roles = $"{nameof(RolType.Propietario)},{nameof(RolType.Administrador)}")]
        public async Task<UnidadGetModel> GetUnidad([FromRoute] int unidadId)
            => await _unidadService.GetUnidad(unidadId);

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = nameof(RolType.Administrador))]
        public async Task<IReadOnlyCollection<UnidadGetModel>> GetUnidadesAdmin()
            => await _unidadService.GetUnidadesAdmin();

        [HttpGet]
        [Authorize(Roles = nameof(RolType.Propietario))]
        public async Task<IReadOnlyCollection<UnidadGetModel>> GetUnidades()
            => await _unidadService.GetUnidades((await _usuarioProvider.GetUser())!.Id);

        [HttpPut]
        [Route("{unidadId}/aprobar")]
        [Authorize(Roles = nameof(RolType.Administrador))]
        public async Task<ActionResult<UnidadGetModel>> AprobarUnidad([FromRoute] int unidadId, [FromBody] AprobarUnidadPutModel model)
        {
            return await _unidadService.AprobarUnidad(unidadId, model);
        }

        [HttpPut]
        [Route("{unidadId}/rechazar")]
        [Authorize(Roles = nameof(RolType.Administrador))]
        public async Task<ActionResult<UnidadGetModel>> RechazarUnidad([FromRoute] int unidadId, [FromBody] RechazarUnidadPutModel model)
        {
            return await _unidadService.RechazarUnidad(unidadId, model);
        }
    }
}
