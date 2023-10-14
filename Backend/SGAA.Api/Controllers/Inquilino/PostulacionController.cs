namespace SGAA.Api.Controllers.Administrador
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
    }
}
