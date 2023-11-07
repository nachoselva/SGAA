namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Auth;

    public interface IUsuarioRepository
    {
        Task<Usuario?> GetUsuarioById(int usuarioId);
        Task<Usuario?> GetUsuarioByEmail(string email);
        Task<IReadOnlyCollection<Usuario>> GetUsuarios();
    }
}
