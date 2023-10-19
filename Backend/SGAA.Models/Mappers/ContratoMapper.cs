namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using System.Text;

    public class ContratoMapper : IContratoMapper
    {
        public ContratoGetModel ToGetModel(Contrato entity)
        =>
            new()
            {
                Id = entity.Id,
                Archivo = Encoding.ASCII.GetString(entity.Archivo)
            };
    }
}
