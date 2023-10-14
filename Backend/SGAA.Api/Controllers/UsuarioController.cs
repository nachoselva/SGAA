namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Providers;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using SGAA.Service.Contracts;
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        public async Task<UsuarioGetModel> GetCurrentUsuario()
        {
            return await _userProvider.GetUser() ?? throw new NotFoundException();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioGetModel>> AddUsuarioPublic([FromBody] UsuarioPostModel model)
        {
            UsuarioGetModel usuario = await _usuarioService.AddUsuarioPublic(model);
            return usuario;
        }

        [HttpPut]
        public async Task<UsuarioGetModel> UpdateUsuario([FromBody] UsuarioPutModel model)
        {
            var currentUser = await _userProvider.GetUser();
            return currentUser != null ? await _usuarioService.UpdateUsuario(currentUser.Id, model) : throw new NotFoundException();
        }

        [HttpGet]
        [Route("confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmUsuario([FromQuery] string email, [FromQuery] string token)
        {
            return Redirect(await _usuarioService.ConfirmUsuario(email, token));
        }
    }
}
