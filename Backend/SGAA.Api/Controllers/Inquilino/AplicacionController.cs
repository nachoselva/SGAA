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
    public class AplicacionController : ControllerBase
    {
        private readonly IUsuarioProvider _usuarioProvider;
        private readonly IAplicacionService _aplicacionService;

        public AplicacionController(IUsuarioProvider usuarioProvider, IAplicacionService aplicacionService)
        {
            _usuarioProvider = usuarioProvider;
            _aplicacionService = aplicacionService;
        }

        [HttpGet]
        public async Task<AplicacionGetModel?> GetActiveAplicacion()
        {
            int inquilinoUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            AplicacionGetModel? postulacion = await _aplicacionService.GetActiveAplicacion(inquilinoUsuarioId);
            return postulacion;
        }

        [HttpPost]
        public async Task<AplicacionGetModel> AddAplicacion([FromBody] AplicacionPostModel model)
        {
            model.InquilinoUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            AplicacionGetModel postulacion = await _aplicacionService.AddAplicacion(model);
            return postulacion;
        }

        [HttpPut]
        public async Task<AplicacionGetModel> UpdateAplicacion([FromBody] AplicacionPutModel model)
        {
            model.InquilinoUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            AplicacionGetModel postulacion = await _aplicacionService.UpdateActiveAplicacion(model);
            return postulacion;
        }
    }
}
