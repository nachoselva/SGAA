namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Api.Middleware;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _usuarioService;

        public SecurityController(ISecurityService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet(Name = "health-check")]
        [AllowAnonymous]
        public IActionResult HealthCheck()
        {
            return Ok("Working fine!");
        }

        [HttpPost]
        [Route("first-usuario")]
        [AllowAnonymous]
        [Transactional]
        public async Task<IActionResult> AddFirstUsuario([FromBody] UsuarioPostModel model)
        {
            return Ok(await _usuarioService.AddFirstUsuario(model));
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [Transactional]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginPostModel model)
        {
            return Ok(await _usuarioService.GetToken(model));
        }

        [HttpPost]
        [Route("refresh-token")]
        [AllowAnonymous]
        [Transactional]
        public async Task<IActionResult> RefreshToken(RefreshTokenPostModel model)
        {
            return Ok(await _usuarioService.RefreshToken(model));
        }

        [HttpPost]
        [Route("revoke")]
        [Authorize]
        [Transactional]
        public async Task<IActionResult> Revoke(RevokeTokenPostModel model)
        {
            await _usuarioService.Revoke(model);
            return NoContent();
        }

        [HttpPost]
        [Route("revoke-all")]
        [Authorize]
        [Transactional]
        public async Task<IActionResult> RevokeAll()
        {
            await _usuarioService.RevokeAll();
            return NoContent();
        }
    }
}
