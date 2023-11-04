namespace SGAA.Api.Controllers.Inquilino
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Middleware;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Inquilino)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Inquilino))]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService _publicacionService;
        private readonly IUsuarioProvider _usuarioProvider;

        public PublicacionController(IPublicacionService publicacionService, IUsuarioProvider usuarioProvider)
        {
            _publicacionService = publicacionService;
            _usuarioProvider = usuarioProvider;
        }

        [HttpGet]
        [Route("{publicacionId}")]
        public async Task<PublicacionGetModel> GetPublicacion([FromRoute] int publicacionId)
            => await _publicacionService.GetPublicacionByInquilino((await _usuarioProvider.GetUser())!.Id, publicacionId);
    }
}
