namespace SGAA.Repository.Contracts
{
    using SGAA.Domain.Auth;

    public interface IUsuarioRepository
    {
        Task<IReadOnlyCollection<Usuario>> GetAllUsuarios();
    }
}
