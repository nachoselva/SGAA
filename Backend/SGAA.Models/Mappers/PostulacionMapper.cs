namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class PostulacionMapper : IPostulacionMapper
    {
        public Postulacion ToEntity(PostulacionPostModel postModel)
        => new(postModel.PublicacionId, postModel.AplicacionId!.Value, PostulacionStatus.Postulada, null);

        public Postulacion ToEntity(RechazarOfertaPostulacionPutModel putModel, Postulacion entity)
        {
            entity.Status = PostulacionStatus.OfertaRechazada;
            entity.FechaOferta = DateTime.Now;
            return entity;
        }

        public Publicacion ToEntity(RechazarOfertaPostulacionPutModel putModel, Publicacion entity)
        {
            entity.Status = PublicacionStatus.Publicada;
            return entity;
        }

        public Aplicacion ToEntity(RechazarOfertaPostulacionPutModel putModel, Aplicacion entity)
        {
            entity.Status = AplicacionStatus.Aprobada;
            return entity;
        }

        public Postulacion ToEntity(AceptarOfertaPostulacionPutModel putModel, Postulacion entity)
        {
            entity.Status = PostulacionStatus.Reservada;
            return entity;
        }

        public Publicacion ToEntity(AceptarOfertaPostulacionPutModel putModel, Publicacion entity)
        {
            entity.Status = PublicacionStatus.Reservada;
            return entity;
        }

        public Aplicacion ToEntity(AceptarOfertaPostulacionPutModel putModel, Aplicacion entity)
        {
            entity.Status = AplicacionStatus.Reservada;
            return entity;
        }

        public Postulacion ToEntity(CancelarPostulacionPutModel putModel, Postulacion entity)
        {
            entity.Status = PostulacionStatus.PostulacionCancelada;
            return entity;
        }

        public PostulacionGetModel ToGetModel(Postulacion entity)
        => new()
        {
            Id = entity.Id,
            PublicacionId = entity.PublicacionId,
            AplicacionId = entity.AplicacionId,
            Status = entity.Status,
            FechaPostulacion = entity.Audit.CreatedOn,
            FechaOferta = entity.FechaOferta,
            MontoAlquiler = entity.Publicacion.MontoAlquiler,
            DomicilioCompleto = entity.Publicacion.Unidad.DomicilioCompleto,
            CanContratoBeCreated = entity.Status == PostulacionStatus.Reservada && !entity.Contratos.Any()
        };
    }
}
