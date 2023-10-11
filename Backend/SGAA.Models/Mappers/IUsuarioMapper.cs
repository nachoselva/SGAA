namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Auth;
    using SGAA.Models.Base;

    public interface IUsuarioMapper : IGetMapper<Usuario, UsuarioGetModel>, IPostMapper<Usuario, UsuarioPostModel>, IPutMapper<Usuario, UsuarioPutModel>
    {

    }
}
