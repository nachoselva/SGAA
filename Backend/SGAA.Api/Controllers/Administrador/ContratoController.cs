namespace SGAA.Api.Controllers.Administrador
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Middleware;
    using SGAA.Domain.Auth;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Administrador)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Administrador))]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoService;

        public ContratoController(IContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        [HttpGet]
        public Task<IReadOnlyCollection<ContratoGetModel>> GetContratos()
            => _contratoService.GetContratos();

        [HttpGet]
        [Route("{contratoId}")]
        public Task<ContratoGetModel> GetContrato([FromRoute] int contratoId)
            => _contratoService.GetContrato(contratoId);

        [HttpPost]
        [Transactional]
        public Task<ContratoGetModel> AddContrato([FromBody] ContratoPostModel model)
            => _contratoService.AddContrato(model);


        [HttpPost]
        [Route("{contratoId}/renovar")]
        [Transactional]
        public Task<ContratoGetModel> RenovarContrato([FromRoute] int contratoId, [FromBody] RenovarContratoPostModel model)
            => _contratoService.RenovarContrato(contratoId, model);

        [HttpPut]
        [Route("{contratoId}/cancelar")]
        [Transactional]
        public Task<ContratoGetModel> CancelarContrato([FromRoute] int contratoId, [FromBody] CancelarContratoPutModel model)
            => _contratoService.CancelarContrato(contratoId, model);
    }
}
