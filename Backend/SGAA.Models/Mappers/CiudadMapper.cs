namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class CiudadMapper : ICiudadMapper
    {
        public CiudadGetModel ToGetModel(Ciudad entity)
        {
            return new CiudadGetModel();
        }
    }
}
