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
    public class PostulacionController : ControllerBase
    {
        private readonly IPostulacionService _postulacionService;

        public PostulacionController(IPostulacionService postulacionService)
        {
            _postulacionService = postulacionService;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<PostulacionGetModel>> GetPostulaciones()
            => await _postulacionService.GetPostulaciones();

        [HttpGet]
        [Route("{postulacionId}")]
        public Task<PostulacionGetModel> GetContrato([FromRoute] int postulacionId)
            => _postulacionService.GetContrato(postulacionId);
    }
}
