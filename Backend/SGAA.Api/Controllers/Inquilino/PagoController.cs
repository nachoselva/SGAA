namespace SGAA.Api.Controllers.Inquilino
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Middleware;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Core;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Inquilino)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Inquilino))]
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
            => await _pagoService.GetPagoByInquilino((await _usuarioProvider.GetUser())!.Id, pagoId);

        [HttpGet]
        public async Task<IReadOnlyCollection<PagoGetModel>> GetPagos()
            => await _pagoService.GetPagosByInquilino((await _usuarioProvider.GetUser())!.Id);

        [HttpGet]
        [Route("Contrato/{contratoId}")]
        public async Task<IReadOnlyCollection<PagoGetModel>> GetPagosByContrato([FromRoute] int contratoId)
            => await _pagoService.GetPagosByInquilinoAndContrato((await _usuarioProvider.GetUser())!.Id, contratoId);

        [HttpPut]
        [Route("{pagoId}/abonar")]
        [Transactional]
        public async Task<ActionResult<PagoGetModel>> AbonarPago([FromRoute] int pagoId, AbonarPagoPutModel model)
        {
            model.InquilinoUsuarioId = (await _usuarioProvider.GetUser())!.Id;
            return await _pagoService.AbonarPago(pagoId, model);
        }

    }
}
