namespace SGAA.Api.Controllers.Propietario
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Propietario)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Propietario))]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService _publicacionService;
        private readonly IUsuarioProvider _usuarioProvider;

        public PublicacionController(IPublicacionService publicacionService, IUsuarioProvider usuarioProvider)
        {
            _publicacionService = publicacionService;
            _usuarioProvider = usuarioProvider;
        }

        [HttpPost]
        public async Task<ActionResult<PublicacionGetModel>> AddPublicacion([FromBody] PublicacionPostModel model)
        {
            model.PropietarioUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            PublicacionGetModel Publicacion = await _publicacionService.AddPublicacion(model);
            return CreatedAtAction(nameof(GetPublicacion), new { PublicacionId = Publicacion.Id }, model);
        }

        [HttpPut]
        [Route("{publicacionId}/cancel")]
        public async Task<ActionResult<PublicacionGetModel>> CancelPublicacion([FromRoute] int publicacionId, [FromBody] PublicacionCancelPutModel model)
        {
            PublicacionGetModel Publicacion = await _publicacionService.CancelPublicacion(publicacionId, model);
            return CreatedAtAction(nameof(GetPublicacion), new { PublicacionId = Publicacion.Id }, model);
        }

        [HttpGet]
        [Route("{PublicacionId}")]
        public async Task<PublicacionGetModel> GetPublicacion([FromRoute] int PublicacionId)
            => await _publicacionService.GetPublicacionByPublicacionId(PublicacionId);
    }
}
