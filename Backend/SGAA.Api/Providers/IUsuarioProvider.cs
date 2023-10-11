namespace SGAA.Api.Providers
{
    using SGAA.Models;

    public interface IUsuarioProvider
    {
        Task<UsuarioGetModel?> GetUser();
    }
}
