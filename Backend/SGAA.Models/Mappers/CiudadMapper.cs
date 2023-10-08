namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class CiudadMapper : ICiudadMapper
    {
        public CiudadGetModel MapFromEntity(Ciudad entity)
        {
            return new CiudadGetModel();
        }
    }
}
