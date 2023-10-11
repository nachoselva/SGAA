namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Providers;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioProvider _userProvider;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioProvider userProvider, IUsuarioService usuarioService)
        {
            _userProvider = userProvider;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        public async Task<UsuarioGetModel> GetUsuario()
        {
            var currentUser = await _userProvider.GetUser();
            if (currentUser == null)
                throw new NotFoundException();
            return currentUser;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioGetModel>> AddUsuarioPublic(UsuarioPostModel model)
        {
            UsuarioGetModel usuario = await _usuarioService.AddUsuarioPublic(model);
            return CreatedAtAction(nameof(GetUsuario), usuario.Id, usuario);
        }

        [HttpPost]
        [Route("admin")]
        [Authorize(Roles = nameof(RolType.Administrador))]
        public async Task<ActionResult<UsuarioGetModel>> AddUsuarioAdmin(UsuarioPostModel model)
        {
            UsuarioGetModel usuario = await _usuarioService.AddUsuario(model);
            return CreatedAtAction(nameof(GetUsuario), usuario.Id, usuario);
        }

        [HttpPut]
        [Authorize]
        public async Task<UsuarioGetModel> UpdateUsuario(UsuarioPutModel model)
        {
            var currentUser = await _userProvider.GetUser();
            if (currentUser == null)
                throw new NotFoundException();
            return await _usuarioService.Update(currentUser.Id, model);
        }
    }
}
