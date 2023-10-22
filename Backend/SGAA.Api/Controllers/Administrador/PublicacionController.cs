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
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService _publicacionService;

        public PublicacionController(IPublicacionService publicacionService)
        {
            _publicacionService = publicacionService;
        }

        [HttpGet]
        [Route("{PublicacionId}")]
        public async Task<PublicacionGetModel> GetPublicacion([FromRoute] int PublicacionId)
            => await _publicacionService.GetPublicacion(PublicacionId);

        [HttpGet]
        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicacionesAdmin()
            => await _publicacionService.GetPublicaciones();
    }
}
