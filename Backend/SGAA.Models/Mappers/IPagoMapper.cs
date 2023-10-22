namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public interface IPagoMapper : 
        IGetMapper<Pago, PagoGetModel>,
        IPostMapper<Pago, PagoPostModel>,
        IPutMapper<Pago, AprobarPagoPutModel>,
        IPutMapper<Pago, AbonarPagoPutModel>
    {

    }
}
