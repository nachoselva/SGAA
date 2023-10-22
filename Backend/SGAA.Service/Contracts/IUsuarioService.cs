namespace SGAA.Service.Contracts
{
    using SGAA.Models;
    using System.Collections.Generic;

    public interface IUsuarioService
    {
        Task<IReadOnlyCollection<UsuarioGetModel>> GetUsuarios();
        Task<UsuarioGetModel> GetUsuario(int usuarioId);
        Task<UsuarioGetModel?> GetUsuario(string email);
        Task<UsuarioGetModel> AddUsuario(UsuarioPostModel model);
        Task<UsuarioGetModel> AddUsuarioPublic(UsuarioPostModel model);
        Task<UsuarioGetModel> UpdateUsuario(int usuarioId, UsuarioPutModel model);
        Task DeleteUsuario(int usuarioId);
        Task<string> ConfirmUsuario(string email, string token);
        Task<UsuarioGetModel> ResetPassword(ResetPasswordPostModel model);
        Task ForgotPassword(ResetPasswordPostModel model);
    }
}
