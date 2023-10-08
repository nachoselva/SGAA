namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Threading.Tasks;

    public interface ISecurityService
    {
        //Task<Usuar> FirstMember(UsuarioAddModel model);
        Task<TokenGetModel> GetToken(UsuarioLoginPostModel model);
        Task<RefreshTokenGetModel> RefreshToken(RefreshTokenPostModel tokenModel);
        Task Revoke(RevokeTokenPostModel model);
        Task RevokeAll();
    }
}
