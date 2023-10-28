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
        [Route("{publicacionId}")]
        public async Task<PublicacionGetModel> GetPublicacion([FromRoute] int publicacionId)
            => await _publicacionService.GetPublicacion(publicacionId);

        [HttpGet]
        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicaciones()
            => await _publicacionService.GetPublicaciones();
    }
}
