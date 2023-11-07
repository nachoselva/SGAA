namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Providers;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
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
        [Route("{codigo}")]
        public async Task<PublicacionGetModel> GetPublicacionActiva([FromRoute] string codigo)
        {
            int? usuarioId = (await _usuarioProvider.GetUser())?.Id;
            return await _publicacionService.GetActivePublicacion(usuarioId, codigo);
        } 


        [HttpGet]
        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicacionesActivas()
        {
            int? usuarioId = (await _usuarioProvider.GetUser())?.Id;
            return  await _publicacionService.GetActivePublicaciones(usuarioId);
        }
    }
}
