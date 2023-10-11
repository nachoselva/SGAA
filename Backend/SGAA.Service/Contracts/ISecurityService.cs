namespace SGAA.Service.Contracts
{
    using SGAA.Models;

    public interface ISecurityService
    {
        Task<UsuarioGetModel> AddFirstUsuario(UsuarioPostModel model);
        Task<TokenGetModel> GetToken(UsuarioLoginPostModel model);
        Task<RefreshTokenGetModel> RefreshToken(RefreshTokenPostModel tokenModel);
        Task Revoke(RevokeTokenPostModel model);
        Task RevokeAll();
    }
}
