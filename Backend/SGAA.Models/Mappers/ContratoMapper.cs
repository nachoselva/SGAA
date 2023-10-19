namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class ContratoMapper : IContratoMapper
    {
        public ContratoGetModel ToGetModel(Contrato entity)
        =>
            new()
            {
                Id = entity.Id
            };
    }
}
