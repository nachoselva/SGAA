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
    [Authorize]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService _contratoService;

        public ContratoController(IContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        [HttpGet]
        public Task<IReadOnlyCollection<ContratoGetModel>> GetContratos()
            => _contratoService.GetContratosAdmin();

        [HttpGet]
        [Route("{contratoId}")]
        public Task<ContratoGetModel?> GetContrato([FromRoute] int contratoId)
            => _contratoService.GetContratoAdmin(contratoId);
    }
}
