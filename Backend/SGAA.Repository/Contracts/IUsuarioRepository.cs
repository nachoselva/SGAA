namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Auth;

    public interface IUsuarioRepository
    {
        Task<Usuario?> GetUsuarioByEmail(string email);
        Task<IReadOnlyCollection<Usuario>> GetUsuarios();
    }
}
