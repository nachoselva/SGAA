namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using System.Text;

    public class PagoMapper : IPagoMapper
    {
        public Pago ToEntity(AprobarPagoPutModel putModel, Pago entity)
        {
            entity.Status = PagoStatus.Aprobado;
            return entity;
        }

        public Pago ToEntity(AbonarPagoPutModel putModel, Pago entity)
        {
            entity.FechaPago = DateTime.Now;
            entity.Archivo = Encoding.ASCII.GetBytes(putModel.Archivo);
            entity.Status = PagoStatus.Abonado;
            return entity;
        }

        public Pago ToEntity(PagoPostModel postModel)
        => new(postModel.ContratoId, postModel.Descripcion, postModel.Monto, postModel.FechaVencimiento, PagoStatus.Pendiente, null, null);

        public PagoGetModel ToGetModel(Pago entity)
        =>
            new()
            {
                ContratoId = entity.ContratoId,
                Descripcion = entity.Descripcion,
                FechaPago = entity.FechaPago,
                FechaVencimiento = entity.FechaVencimiento,
                Monto = entity.Monto,
                Status = entity.Status,
                Archivo = entity.Archivo
            };
    }
}
