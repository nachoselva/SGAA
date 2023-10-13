namespace SGAA.Api.Controllers
{
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
            var currentUser = await _userProvider.GetUser();
            if (currentUser == null)
                throw new NotFoundException();
            return currentUser;
        }

        [HttpGet]
        [Route("{usuarioId}")]
        [Authorize(Roles = nameof(RolType.Administrador))]
        public async Task<UsuarioGetModel> GetUsuario([FromRoute] int usuarioId)
        {
            var currentUser = await _usuarioService.GetById(usuarioId);
            if (currentUser == null)
                throw new NotFoundException();
            return currentUser;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioGetModel>> AddUsuarioPublic([FromBody] UsuarioPostModel model)
        {
            UsuarioGetModel usuario = await _usuarioService.AddUsuarioPublic(model);
            return CreatedAtAction(nameof(GetUsuario), new { usuarioId = usuario.Id }, usuario);
        }

        [HttpPost]
        [Route("admin")]
        [Authorize(Roles = nameof(RolType.Administrador))]
        public async Task<ActionResult<UsuarioGetModel>> AddUsuarioAdmin([FromBody] UsuarioPostModel model)
        {
            UsuarioGetModel usuario = await _usuarioService.AddUsuario(model);
            return CreatedAtAction(nameof(GetUsuario), new { usuarioId = usuario.Id }, usuario);
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
        public async Task<IActionResult> ConfirmUsuario(string email, string token)
        {
            return Redirect(await _usuarioService.ConfirmUsuario(email, token));
        }
    }
}
