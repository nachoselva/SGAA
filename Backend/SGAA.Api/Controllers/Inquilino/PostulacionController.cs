namespace SGAA.Api.Controllers.Inquilino
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Inquilino)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Inquilino))]
    public class PostulacionController : ControllerBase
    {
        private readonly IUsuarioProvider _usuarioProvider;
        private readonly IPostulacionService _postulacionService;

        public PostulacionController(IUsuarioProvider usuarioProvider, IPostulacionService postulacionService)
        {
            _usuarioProvider = usuarioProvider;
            _postulacionService = postulacionService;
        }

        [HttpPost]
        public async Task<PostulacionGetModel> AddPostulacion([FromBody] PostulacionPostModel model)
        {
            model.InquilinoUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            PostulacionGetModel postulacion = await _postulacionService.AddPostulacion(model);
            return postulacion;
        }

        [HttpPut]
        [Route("{postulacionId}/aceptar-oferta")]
        public async Task<PostulacionGetModel> AceptarOferta([FromRoute] int postulacionId, [FromBody] AceptarOfertaPostulacionPutModel model)
        {
            model.InquilinoUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            PostulacionGetModel postulacion = await _postulacionService.AceptarOferta(postulacionId, model);
            return postulacion;
        }

        [HttpPut]
        [Route("{postulacionId}/rechazar-oferta")]
        public async Task<PostulacionGetModel> RechazarOferta([FromRoute] int postulacionId, [FromBody] RechazarOfertaPostulacionPutModel model)
        {
            model.InquilinoUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            PostulacionGetModel postulacion = await _postulacionService.RechazarOferta(postulacionId, model);
            return postulacion;
        }

        [HttpPut]
        [Route("{postulacionId}/cancelar")]
        public async Task<PostulacionGetModel> CancelarPostulacion([FromRoute] int postulacionId, [FromBody] CancelarPostulacionPutModel model)
        {
            model.InquilinoUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            PostulacionGetModel postulacion = await _postulacionService.CancelarPostulacion(postulacionId, model);
            return postulacion;
        }
    }
}
