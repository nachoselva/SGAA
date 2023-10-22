namespace SGAA.Api.Providers
{
    using Microsoft.Extensions.Primitives;
    using SGAA.Models;
    using SGAA.Service.Contracts;
    using System.Net;
    using System.Security.Claims;

    public class UsuarioProvider : IUsuarioProvider
    {
        private const string FORWARDED_FOR_HEADER = "X-Forwarded-For";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsuarioService _usuarioService;

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
            return await _usuarioService.GetUsuario(email!);
        }

        public string GetDireccionIp()
        {
            HttpContext context = _httpContextAccessor.HttpContext!;
            IPAddress? ipAddress = context.Connection.RemoteIpAddress;
            if (context.Request.Headers.TryGetValue(FORWARDED_FOR_HEADER, out StringValues forwardedValue))
            {
                string[] split = forwardedValue.ToString().Split(new char[] { ',' });
                if (split.Length > 0)
                {
                    string ip = split[0];
                    if (IPAddress.TryParse(ip, out IPAddress? address))
                        ipAddress = address;
                }
            }
            return ipAddress?.ToString() ?? string.Empty;
        }
    }
}
