namespace SGAA.Service
{
    using Microsoft.SqlServer.Server;
    using SGAA.Documents.Contracts;
    using SGAA.Documents.DocumentModels;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using System.Threading.Tasks;

    public class PostulacionService : IPostulacionService
    {
        private readonly IPostulacionRepository _postulacionRepository;
        private readonly IPublicacionRepository _publicacionRepository;
        private readonly IAplicacionRepository _aplicacionRepository;
        private readonly IPostulacionMapper _postulacionMapper;
        private readonly IPostulacionCreadaEmailSender _postulacionCreadaEmailSender;
        private readonly IOfertaAceptadaEmailSender _ofertaAceptadaEmailSender;
        private readonly IOfertaRechazadaEmailSender _ofertaRechazadaEmailSender;
        private readonly IContratoService _contratoService;

        public PostulacionService(IPostulacionRepository postulacionRepository, IPublicacionRepository publicacionRepository,
            IAplicacionRepository aplicacionRepository, IPostulacionMapper postulacionMapper,
            IPostulacionCreadaEmailSender postulacionCreadaEmailSender, IOfertaAceptadaEmailSender ofertaAceptadaEmailSender,
            IOfertaRechazadaEmailSender ofertaRechazadaEmailSender, IContratoService contratoService)
        {
            _postulacionRepository = postulacionRepository;
            _publicacionRepository = publicacionRepository;
            _aplicacionRepository = aplicacionRepository;
            _postulacionMapper = postulacionMapper;
            _postulacionCreadaEmailSender = postulacionCreadaEmailSender;
            _ofertaAceptadaEmailSender = ofertaAceptadaEmailSender;
            _ofertaRechazadaEmailSender = ofertaRechazadaEmailSender;
            _contratoService = contratoService;
        }

        public async Task<PostulacionGetModel> AddPostulacion(PostulacionPostModel model)
        {
            IReadOnlyCollection<Aplicacion> aplicaciones = await _aplicacionRepository
                .GetAplicacionesByInquilinoUsuarioId(model.InquilinoUsuarioId!.Value);

            Aplicacion? activeAplicacion = aplicaciones.FirstOrDefault(ap => ap.Status == AplicacionStatus.Aprobada)
                ?? throw new BadRequestException("Aplicacion", "No tiene una aplicación aprobada para poder postular");
            Publicacion? publicacion = await _publicacionRepository.GetPublicacionById(model.PublicacionId);

            if (publicacion == null || publicacion.Status != PublicacionStatus.Publicada)
                throw new BadRequestException("Publicacion", "La publicación no está disponible");
            model.AplicacionId = activeAplicacion.Id;

            if(activeAplicacion.Postulaciones.Any(p => p.Status.IsActive() && p.PublicacionId == model.PublicacionId))
                throw new BadRequestException("Postulación", "La aplicación activa ya postuló para esta unidad");

            Postulacion postulacion = model.ToEntity(_postulacionMapper);
            postulacion = await _postulacionRepository.AddPostulacion(postulacion);

            await _postulacionCreadaEmailSender.SendEmail(publicacion.Unidad.PropietarioUsuario.Email!,
                new PostulacionCreadaEmailModel
                {
                    Nombre = publicacion.Unidad.PropietarioUsuario.Nombre,
                    Apellido = publicacion.Unidad.PropietarioUsuario.Apellido,
                    Domicilio = publicacion.Unidad.DomicilioCompleto
                });

            return postulacion.MapToGetModel(_postulacionMapper);
        }

        public async Task<PostulacionGetModel> AceptarOferta(int postulacionId, AceptarOfertaPostulacionPutModel model)
        {
            Postulacion? postulacion = await _postulacionRepository.GetPostulacionById(postulacionId);
            if (postulacion == null || postulacion.Aplicacion.InquilinoUsuarioId != model.InquilinoUsuarioId)
                throw new NotFoundException();
            if (postulacion.Status != PostulacionStatus.Ofrecida)
                throw new BadRequestException(nameof(postulacion.Status), "La postulación no se encuentra en estado para aceptar");
            if (model.FechaDesde >= model.FechaHasta)
                throw new BadRequestException(nameof(model.FechaHasta), "Fecha hasta desde ser posterior a fecha desde");
            model.ToEntity(_postulacionMapper, postulacion.Publicacion);
            model.ToEntity(_postulacionMapper, postulacion.Aplicacion);
            postulacion = model.ToEntity(_postulacionMapper, postulacion);
            postulacion = await _postulacionRepository.UpdatePostulacion(postulacion);
            Unidad unidad = postulacion.Publicacion.Unidad;
            Usuario inquilinoUsuario = postulacion.Aplicacion.InquilinoUsuario;
            Usuario propietarioUsuario = postulacion.Publicacion.Unidad.PropietarioUsuario;

            await _ofertaAceptadaEmailSender.SendEmail(inquilinoUsuario.Email!,
                new OfertaAceptadaEmailModel
                {
                    Nombre = inquilinoUsuario.Nombre,
                    Apellido = inquilinoUsuario.Apellido,
                    Domicilio = unidad.DomicilioCompleto
                });

            await _ofertaAceptadaEmailSender.SendEmail(propietarioUsuario.Email!,
                new OfertaAceptadaEmailModel
                {
                    Nombre = propietarioUsuario.Nombre,
                    Apellido = propietarioUsuario.Apellido,
                    Domicilio = unidad.DomicilioCompleto
                });

            await _contratoService.CreateContrato(postulacion.Id, model.FechaDesde, model.FechaHasta);

            return postulacion.MapToGetModel(_postulacionMapper);
        }

        public async Task<PostulacionGetModel> RechazarOferta(int postulacionId, RechazarOfertaPostulacionPutModel model)
        {
            Postulacion? postulacion = await _postulacionRepository.GetPostulacionById(postulacionId);
            if (postulacion == null || postulacion.Aplicacion.InquilinoUsuarioId != model.InquilinoUsuarioId)
                throw new NotFoundException();
            if (postulacion.Status != PostulacionStatus.Ofrecida)
                throw new BadRequestException(nameof(postulacion.Status), "La postulación no se encuentra en estado para rechazar");
            model.ToEntity(_postulacionMapper, postulacion.Publicacion);
            model.ToEntity(_postulacionMapper, postulacion.Aplicacion);
            postulacion = model.ToEntity(_postulacionMapper, postulacion);
            postulacion = await _postulacionRepository.UpdatePostulacion(postulacion);

            Unidad unidad = postulacion.Publicacion.Unidad;
            Usuario propietarioUsuario = postulacion.Publicacion.Unidad.PropietarioUsuario;

            await _ofertaRechazadaEmailSender.SendEmail(propietarioUsuario.Email!,
                new OfertaRechazadaEmailModel
                {
                    Nombre = propietarioUsuario.Nombre,
                    Apellido = propietarioUsuario.Apellido,
                    Domicilio = unidad.DomicilioCompleto
                });

            return postulacion.MapToGetModel(_postulacionMapper);
        }

        public async Task<PostulacionGetModel> CancelarPostulacion(int postulacionId, CancelarPostulacionPutModel model)
        {
            Postulacion? postulacion = await _postulacionRepository.GetPostulacionById(postulacionId);
            if (postulacion == null || postulacion.Aplicacion.InquilinoUsuarioId != model.InquilinoUsuarioId)
                throw new NotFoundException();
            if (postulacion.Status != PostulacionStatus.Postulada)
                throw new BadRequestException(nameof(postulacion.Status), "La postulación no se encuentra en estado para cancelar");
            if (postulacion.Aplicacion.Status != AplicacionStatus.Aprobada)
                throw new BadRequestException(nameof(postulacion.Status), "La aplicación no se encuentra en estado para cancelar");
            postulacion = model.ToEntity(_postulacionMapper, postulacion);
            postulacion = await _postulacionRepository.UpdatePostulacion(postulacion);

            return postulacion.MapToGetModel(_postulacionMapper);
        }
    }
}
