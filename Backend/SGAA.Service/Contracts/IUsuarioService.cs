namespace SGAA.Service.Contracts
{
    using SGAA.Models;

    public interface IUsuarioService
    {
        Task<UsuarioGetModel?> GetById(int email);
        Task<UsuarioGetModel?> GetByEmail(string email);
        Task<UsuarioGetModel> AddUsuario(UsuarioPostModel model);
        Task<UsuarioGetModel> AddUsuarioPublic(UsuarioPostModel model);
        Task<UsuarioGetModel> Update(int id, UsuarioPutModel model);
        Task Delete(int id);
    }
}
