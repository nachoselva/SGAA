namespace SGAA.Service.Contracts
{
    using SGAA.Models;

    public interface IUsuarioService
    {
        Task<UsuarioGetModel?> GetById(int email);
        Task<UsuarioGetModel?> GetByEmail(string email);
        Task<UsuarioGetModel> AddUsuario(UsuarioPostModel model);
        Task<UsuarioGetModel> AddUsuarioPublic(UsuarioPostModel model);
        Task<UsuarioGetModel> UpdateUsuario(int id, UsuarioPutModel model);
        Task DeleteUsuario(int id);
        Task<string> ConfirmUsuario(string email, string token);
        Task<UsuarioGetModel> ResetPassword(ResetPasswordPostModel model);
        Task ForgotPassword(ResetPasswordPostModel model);
    }
}
