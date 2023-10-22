namespace SGAA.Api.Controllers.Propietario
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Core;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Propietario)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Propietario))]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;
        private readonly IUsuarioProvider _usuarioProvider;

        public PagoController(IPagoService pagoService, IUsuarioProvider usuarioProvider)
        {
            _pagoService = pagoService;
            _usuarioProvider = usuarioProvider;
        }

        [HttpGet]
        [Route("{pagoId}")]
        public async Task<PagoGetModel> GetPagos([FromRoute] int pagoId)
            => await _pagoService.GetPagoByPropietario((await _usuarioProvider.GetUser())!.Id, pagoId);

        [HttpGet]
        public async Task<IReadOnlyCollection<PagoGetModel>> GetPagos()
            => await _pagoService.GetPagosByPropietario((await _usuarioProvider.GetUser())!.Id);

        [HttpGet]
        [Route("Contrato/{contratoId}")]
        public async Task<IReadOnlyCollection<PagoGetModel>> GetPagosByContrato([FromRoute] int contratoId)
            => await _pagoService.GetPagosByPropietarioAndContrato((await _usuarioProvider.GetUser())!.Id, contratoId);

        [HttpPost]
        public async Task<ActionResult<PagoGetModel>> AddPago([FromBody] PagoPostModel model)
        {
            model.PropietarioUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            return await _pagoService.AddPago(model);
        }

        [HttpPut]
        [Route("{pagoId}/aprobar")]
        public async Task<ActionResult<PagoGetModel>> AprobarPago([FromRoute] int pagoId, AprobarPagoPutModel model)
        {
            model.PropietarioUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            return await _pagoService.AprobarPago(pagoId, model);
        }

    }
}
