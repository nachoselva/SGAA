namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService _publicacionService;

        public PublicacionController(IPublicacionService publicacionService)
        {
            _publicacionService = publicacionService;
        }

        [HttpPost]
        [Authorize(Roles = nameof(RolType.Propietario))]
        public async Task<ActionResult<PublicacionGetModel>> AddPublicacion([FromBody] PublicacionPostModel model)
        {
            PublicacionGetModel Publicacion = await _publicacionService.AddPublicacion(model);
            return CreatedAtAction(nameof(GetPublicacion), new { PublicacionId = Publicacion.Id }, model);
        }

        [HttpPut]
        [Route("{publicacionId}/cancel")]
        [Authorize(Roles = nameof(RolType.Propietario))]
        public async Task<ActionResult<PublicacionGetModel>> CancelPublicacion([FromRoute] int publicacionId, [FromBody] PublicacionCancelPutModel model)
        {
            PublicacionGetModel Publicacion = await _publicacionService.CancelPublicacion(publicacionId, model);
            return CreatedAtAction(nameof(GetPublicacion), new { PublicacionId = Publicacion.Id }, model);
        }

        [HttpGet]
        [Route("{PublicacionId}")]
        [Authorize(Roles = $"{nameof(RolType.Propietario)},{nameof(RolType.Administrador)}")]
        public async Task<PublicacionGetModel> GetPublicacion([FromRoute] int PublicacionId)
            => await _publicacionService.GetPublicacion(PublicacionId);

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = nameof(RolType.Administrador))]
        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicacionesAdmin()
            => await _publicacionService.GetPublicacionesAdmin();
    }
}
