namespace SGAA.Api.Providers
{
    using SGAA.Models;
    using System.Net;

    public interface IUsuarioProvider
    {
        Task<UsuarioGetModel?> GetUser();
        string GetDireccionIp();
    }
}
