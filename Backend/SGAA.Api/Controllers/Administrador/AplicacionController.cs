namespace SGAA.Api.Controllers.Administrador
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Administrador)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Administrador))]
    public class AplicacionController : ControllerBase
    {
        private readonly IAplicacionService _aplicacionService;

        public AplicacionController(IAplicacionService aplicacionService)
        {
            _aplicacionService = aplicacionService;
        }

        [HttpGet]
        [Route("{aplicacionId}")]
        public async Task<AplicacionGetModel> GetAplicacion([FromRoute] int aplicacionId)
            => await _aplicacionService.GetAplicacion(aplicacionId);

        [HttpGet]
        public async Task<IReadOnlyCollection<AplicacionGetModel>> GetAplicacionesAdmin()
            => await _aplicacionService.GetAplicaciones();

        [HttpPut]
        [Route("{aplicacionId}/aprobar")]
        public async Task<ActionResult<AplicacionGetModel>> AprobarAplicacion([FromRoute] int aplicacionId, [FromBody] AprobarAplicacionPutModel model)
            => await _aplicacionService.AprobarAplicacion(aplicacionId, model);

        [HttpPut]
        [Route("{aplicacionId}/rechazar")]
        public async Task<ActionResult<AplicacionGetModel>> RechazarAplicacion([FromRoute] int aplicacionId, [FromBody] RechazarAplicacionPutModel model)
            => await _aplicacionService.RechazarAplicacion(aplicacionId, model);
    }
}
