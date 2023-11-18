namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Middleware;
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
        [Route("current")]
        public async Task<UsuarioGetModel> GetCurrentUsuario()
        {
            return await _userProvider.GetUser() ?? throw new NotFoundException();
        }

        [HttpPost]
        [AllowAnonymous]
        [Transactional]
        public async Task<ActionResult<UsuarioGetModel>> AddUsuarioPublic([FromBody] UsuarioPostModel model)
        {
            UsuarioGetModel usuario = await _usuarioService.AddUsuarioPublic(model);
            return usuario;
        }

        [HttpPut]
        [Transactional]
        public async Task<UsuarioGetModel> UpdateUsuario([FromBody] UsuarioPutModel model)
        {
            var currentUser = await _userProvider.GetUser();
            return currentUser != null ? await _usuarioService.UpdateUsuario(currentUser.Id, model) : throw new NotFoundException();
        }

        [HttpPost]
        [Route("confirm")]
        [AllowAnonymous]
        [Transactional]
        public async Task<IActionResult> ConfirmUsuario([FromBody] ConfirmUsuarioPostModel model)
        {
            await _usuarioService.ConfirmUsuario(model);
            return Ok();
        }

        [HttpPost]
        [Route("reset-password")]
        [AllowAnonymous]
        [Transactional]
        public async Task<ActionResult<UsuarioGetModel>> ResetPassword([FromBody] ResetPasswordPostModel model)
        {
            UsuarioGetModel usuario = await _usuarioService.ResetPassword(model);
            return usuario;
        }

        [HttpPost]
        [Route("forgot-password")]
        [AllowAnonymous]
        [Transactional]
        public async Task<ActionResult<UsuarioGetModel>> ForgotPassword([FromBody] ForgotPasswordPostModel model)
        {
            await _usuarioService.ForgotPassword(model);
            return Ok();
        }
    }
}
