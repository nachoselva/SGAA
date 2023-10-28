namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Auth;

    public class UsuarioMapper : IUsuarioMapper
    {
        public Usuario ToEntity(UsuarioPostModel postModel)
        {
            return new(postModel.Email, postModel.Nombre, postModel.Apellido, Guid.NewGuid().ToString(), null, null)
            {
                UserName = postModel.Email
            };
        }

        public Usuario ToEntity(UsuarioPutModel putModel, Usuario usuario)
        {
            usuario.Nombre = putModel.Nombre;
            usuario.Apellido = putModel.Apellido;
            return usuario;
        }

        public UsuarioGetModel ToGetModel(Usuario entity)
        {
            return new UsuarioGetModel()
            {
                Id = entity.Id,
                Email = entity.Email!,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Roles = string.Join(',', entity.UsuarioRoles?.Select(ur => ur.Rol.Name) ?? Array.Empty<string>()) 
            };
        }
    }
}
