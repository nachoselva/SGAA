namespace SGAA.Api.Controllers.Administrador
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Middleware;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Administrador)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Administrador))]
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
        public async Task<PagoGetModel> GetPago([FromRoute] int pagoId)
            => await _pagoService.GetPago(pagoId);

        [HttpGet]
        public async Task<IReadOnlyCollection<PagoGetModel>> GetPagos()
            => await _pagoService.GetPagos();

    }
}
