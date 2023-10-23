namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public interface IContratoMapper :
        IGetMapper<Contrato, ContratoGetModel>,
        IPutMapper<Contrato, CancelarContratoPutModel>,
        IPutMapper<Unidad, CancelarContratoPutModel>
    {

    }
}
