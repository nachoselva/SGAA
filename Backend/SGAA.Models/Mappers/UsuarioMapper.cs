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

        public UsuarioGetModel ToGetModel(Usuario entity)
        {
            return new UsuarioGetModel()
            {
                Id = entity.Id,
                Email = entity.Email!,
                FirstName = entity.Nombre,
                LastName = entity.Nombre
            };
        }
    }
}
