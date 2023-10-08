namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class ProvinciaMapper : IProvinciaMapper
    {
        public ProvinciaGetModel MapFromEntity(Provincia entity)
        {
            return new ProvinciaGetModel();
        }
    }
}
