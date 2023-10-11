namespace SGAA.Api.Providers
{
    using SGAA.Models;
    using SGAA.Service.Contracts;
    using System.Security.Claims;

    public class UsuarioProvider : IUsuarioProvider
    {
        IHttpContextAccessor _httpContextAccessor;
        IUsuarioService _usuarioService;

        public UsuarioProvider(IHttpContextAccessor httpContextAccessor, IUsuarioService usuarioService)
        {
            _httpContextAccessor = httpContextAccessor;
            _usuarioService = usuarioService;
        }

        public async Task<UsuarioGetModel?> GetUser()
        {
            string? email = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(email))
                return null;
            return await _usuarioService.GetByEmail(email!);
        }
    }
}
