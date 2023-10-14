namespace SGAA.Service
{
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

        public PostulacionService(IPostulacionRepository postulacionRepository, IPublicacionRepository publicacionRepository,
            IAplicacionRepository aplicacionRepository, IPostulacionMapper postulacionMapper,
            IPostulacionCreadaEmailSender postulacionCreadaEmailSender)
        {
            _postulacionRepository = postulacionRepository;
            _publicacionRepository = publicacionRepository;
            _aplicacionRepository = aplicacionRepository;
            _postulacionMapper = postulacionMapper;
            _postulacionCreadaEmailSender = postulacionCreadaEmailSender;
        }

        public async Task<PostulacionGetModel> AddPostulacion(PostulacionPostModel model)
        {
            IReadOnlyCollection<Aplicacion> aplicaciones = await _aplicacionRepository
                .GetAplicacionesByInquilinoUsuarioId(model.InquilinoUsuarioId!.Value);

            Aplicacion? activeAplicacion = aplicaciones.FirstOrDefault(ap => ap.Status == AplicacionStatus.Aprobada);
            if (activeAplicacion == null)
                throw new BadRequestException("Aplicacion", "No tiene una aplicación aprobada para poder postular");

            Publicacion? publicacion = await _publicacionRepository.GetPublicacionById(model.PublicacionId);

            if (publicacion == null || publicacion.Status != PublicacionStatus.Publicada)
                throw new BadRequestException("Publicacion", "La publicación no está disponible");

            Postulacion postulacion = model.ToEntity(_postulacionMapper);
            postulacion = await _postulacionRepository.AddPostulacion(postulacion);

            await _postulacionCreadaEmailSender.SendEmail(publicacion.Unidad.PropietarioUsuario.Email!,
                new PostulacionCreadaEmailModel
                {
                    Nombre = activeAplicacion.InquilinoUsuario.Nombre,
                    Apellido = activeAplicacion.InquilinoUsuario.Apellido,
                    Domicilio = publicacion.Unidad.DomicilioCompleto
                });

            return postulacion.MapToGetModel(_postulacionMapper);
        }
    }
}
