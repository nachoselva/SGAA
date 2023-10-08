namespace SGAA.Service.Contracts
{
    using SGAA.Models;

    public interface IUsuarioService
    {
        Task<UsuarioGetModel> FirstMember(UsuarioPostModel model);

        Task<TokenGetModel> GetToken(UsuarioLoginPostModel model);
        Task<RefreshTokenGetModel> RefreshToken(RefreshTokenPostModel tokenModel);
        Task Revoke(RevokeTokenPostModel model);
        Task RevokeAll();
    }
}
