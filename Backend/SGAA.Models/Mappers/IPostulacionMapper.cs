namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public interface IPostulacionMapper : IGetMapper<Postulacion, PostulacionGetModel>, IPostMapper<Postulacion, PostulacionPostModel>
    {

    }
}
