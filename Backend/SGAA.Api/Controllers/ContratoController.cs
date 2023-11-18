namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Middleware;
    using SGAA.Api.Providers;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoService;
        private readonly IUsuarioProvider _usuarioProvider;

        public ContratoController(IContratoService contratoService, IUsuarioProvider usuarioProvider)
        {
            _contratoService = contratoService;
            _usuarioProvider = usuarioProvider;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<ContratoGetModel>> GetContratos()
        {
            int usuarioId = (await _usuarioProvider.GetUser())!.Id;
            return await _contratoService.GetContratos(usuarioId);
        }

        [HttpGet]
        [Route("{contratoId}")]
        public async Task<ContratoGetModel> GetContrato([FromRoute] int contratoId)
        {
            int usuarioId = (await _usuarioProvider.GetUser())!.Id;
            return await _contratoService.GetContrato(usuarioId, contratoId);
        }

        [HttpPut]
        [Route("{contratoId}/firmar")]
        [Transactional]
        public async Task<ContratoGetModel> FirmarContrato([FromRoute] int contratoId)
        {
            string direccionIP = _usuarioProvider.GetDireccionIp();
            int usuarioId = (await _usuarioProvider.GetUser())!.Id;
            return await _contratoService.FirmarContrato(usuarioId, contratoId, direccionIP);
        }
    }
}
