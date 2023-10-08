namespace SGAA.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SGAA.Models;
    using SGAA.Service.Contracts;

    [ApiController]
    [Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public SecurityController(IUsuarioService usuarioService)
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
        [Route("first-member")]
        [AllowAnonymous]
        public async Task<IActionResult> FirstMember([FromBody] UsuarioPostModel model)
        {
            return Ok(await _usuarioService.FirstMember(model));
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginPostModel model)
        {
            return Ok(await _usuarioService.GetToken(model));
        }

        [HttpPost]
        [Route("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenPostModel model)
        {
            return Ok(await _usuarioService.RefreshToken(model));
        }

        [HttpPost]
        [Route("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke(RevokeTokenPostModel model)
        {
            await _usuarioService.Revoke(model);
            return NoContent();
        }

        [HttpPost]
        [Route("revoke-all")]
        [Authorize]
        public async Task<IActionResult> RevokeAll()
        {
            await _usuarioService.RevokeAll();
            return NoContent();
        }
    }
}
