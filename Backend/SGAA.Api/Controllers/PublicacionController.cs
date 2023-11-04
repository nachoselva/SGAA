namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class PublicacionController : ControllerBase
    {
        private readonly IPublicacionService _publicacionService;

        public PublicacionController(IPublicacionService publicacionService)
        {
            _publicacionService = publicacionService;
        }

        [HttpGet]
        [Route("{codigo}")]
        public async Task<PublicacionGetModel> GetPublicacionActiva([FromRoute] string codigo)
            => await _publicacionService.GetActivePublicacion(codigo);


        [HttpGet]
        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicacionesActivas()
            => await _publicacionService.GetActivePublicaciones();
    }
}
