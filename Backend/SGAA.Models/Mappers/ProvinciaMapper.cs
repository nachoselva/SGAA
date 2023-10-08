namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class ProvinciaMapper : IProvinciaMapper
    {
        public ProvinciaGetModel ToGetModel(Provincia entity)
        {
            return new ProvinciaGetModel();
        }
    }
}
