namespace SGAA.Api.Controllers.Administrador
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route($"{nameof(RolType.Administrador)}/[controller]")]
    [Authorize(Roles = nameof(RolType.Administrador))]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("{usuarioId}")]
        public async Task<UsuarioGetModel> GetUsuario([FromRoute] int usuarioId)
            => await _usuarioService.GetById(usuarioId) ?? throw new NotFoundException();

        [HttpPost]
        public async Task<UsuarioGetModel> AddUsuarioAdmin([FromBody] UsuarioPostModel model)
            => await _usuarioService.AddUsuario(model);
    }
}
