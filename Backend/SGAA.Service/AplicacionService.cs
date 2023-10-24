namespace SGAA.Service
{
    using SGAA.Domain.Auth;
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Models;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using System;
    using System.Threading.Tasks;

    public class AplicacionService : IAplicacionService
    {
        private readonly IAplicacionRepository _aplicacionRepository;
        private readonly IAplicacionMapper _aplicacionMapper;
        private readonly IAprobarAplicacionEmailSender _aprobarAplicacionEmailSender;
        private readonly IRechazarAplicacionEmailSender _rechazarAplicacionEmailSender;

        public AplicacionService(IAplicacionRepository aplicacionRepository, IAplicacionMapper aplicacionMapper,
            IAprobarAplicacionEmailSender aprobarAplicacionEmailSender, IRechazarAplicacionEmailSender rechazarAplicacionEmailSender)
        {
            _aplicacionRepository = aplicacionRepository;
            _aplicacionMapper = aplicacionMapper;
            _aprobarAplicacionEmailSender = aprobarAplicacionEmailSender;
            _rechazarAplicacionEmailSender = rechazarAplicacionEmailSender;
        }
        private async Task<Aplicacion> UpsertGarantias(AplicacionPutModel putModel, Aplicacion aplicacion)
        {
            GarantiaModel[] updatedGarantiaModels = putModel.Garantias.Where(img => img.Id.HasValue && img.Id.Value > 0).ToArray();
            GarantiaModel[] newGarantiaModels = putModel.Garantias.Except(updatedGarantiaModels).ToArray();
            int[] notDeletedIds = updatedGarantiaModels.Where(img => img.Id.HasValue).Select(img => img.Id!.Value).ToArray();

            //Add garantias

            aplicacion.AddGarantias(newGarantiaModels.Select(newmodel => newmodel.ToEntity<Garantia, GarantiaModel>(_aplicacionMapper)));

            //Update garantias

            foreach (var updateGarantiaModel in updatedGarantiaModels)
            {
                Garantia garantia = aplicacion.Garantias.First(img => img.Id == updateGarantiaModel.Id);
                garantia = updateGarantiaModel.ToEntity(_aplicacionMapper, garantia);
            }

            //Delete garantias

            Garantia[] entitiesToDelete = aplicacion.Garantias
                .Where(entity => entity.Id > 0)
                .Where(entity => !notDeletedIds.Contains(entity.Id))
                .ToArray();

            aplicacion.RemoveGarantias(entitiesToDelete);

            if (entitiesToDelete.Any())
                await _aplicacionRepository.DeleteGarantias(entitiesToDelete);

            return aplicacion;
        }

        private async Task<Aplicacion> UpsertPostulantes(AplicacionPutModel putModel, Aplicacion aplicacion)
        {
            PostulanteModel[] updatedPostulacionModels = putModel.Postulantes.Where(img => img.Id.HasValue && img.Id.Value > 0).ToArray();
            PostulanteModel[] newPostulacionModels = putModel.Postulantes.Except(updatedPostulacionModels).ToArray();
            int[] notDeletedIds = updatedPostulacionModels.Where(img => img.Id.HasValue).Select(img => img.Id!.Value).ToArray();

            //Add postulantes

            aplicacion.AddPostulantes(newPostulacionModels.Select(newmodel => newmodel.ToEntity<Postulante, PostulanteModel>(_aplicacionMapper)));

            //Update postulantes

            foreach (var updatePostulacionModel in updatedPostulacionModels)
            {
                Postulante postulacion = aplicacion.Postulantes.First(img => img.Id == updatePostulacionModel.Id);
                postulacion = updatePostulacionModel.ToEntity(_aplicacionMapper, postulacion);
            }

            //Delete postulantes

            Postulante[] entitiesToDelete = aplicacion.Postulantes
                .Where(entity => entity.Id > 0)
                .Where(entity => !notDeletedIds.Contains(entity.Id))
                .ToArray();

            aplicacion.RemovePostulantes(entitiesToDelete);

            if (entitiesToDelete.Any())
                await _aplicacionRepository.DeletePostulantes(entitiesToDelete);

            return aplicacion;
        }

        private static decimal CalculatePuntuacionTotal(Aplicacion aplicacion, decimal iclIndice)
        {
            decimal montoGarantia = aplicacion.Garantias.Sum(g => g.Monto);
            decimal montoIngresos = aplicacion.Postulantes.Sum(g => g.IngresoMensual);
            decimal indiceHistorialBancario = aplicacion.Postulantes.Average(p => (decimal)p.PuntuacionCrediticia!.Value);
            decimal indiceAntecedentesPenales = aplicacion.Postulantes.Average(p => (decimal)p.PuntuacionCrediticia!.Value);
            return
                (indiceHistorialBancario / 1000)
                * (indiceAntecedentesPenales / 1000)
                * (2 * montoGarantia * 10e-3m + 6 * montoGarantia * 10e-4m)
                / iclIndice;
        }

        public async Task<AplicacionGetModel> GetActiveAplicacion(int inquilinoUsuarioId)
        {
            IReadOnlyCollection<Aplicacion> aplicaciones = await _aplicacionRepository
                .GetAplicaciones(inquilinoUsuarioId);

            Aplicacion? aplicacion = aplicaciones
                .FirstOrDefault(ap => ap.Status.IsActive());
            if (aplicacion == null || aplicacion.Status != AplicacionStatus.AprobacionPendiente)
                throw new NotFoundException();
            return aplicacion.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper);
        }

        public async Task<AplicacionGetModel> AddAplicacion(AplicacionPostModel postModel)
        {
            IReadOnlyCollection<Aplicacion> aplicaciones = await _aplicacionRepository
                .GetAplicaciones(postModel.InquilinoUsuarioId!.Value);

            Aplicacion? existingAplicacion = aplicaciones
                .FirstOrDefault(ap => ap.Status.IsActive());
            if (existingAplicacion != null)
                throw new BadRequestException("Aplicacion", "Existe una aplicación activa, no es posible crear otra aplicación");

            Aplicacion aplicacion = postModel.ToEntity<Aplicacion, AplicacionPostModel>(_aplicacionMapper);
            aplicacion.AddPostulantes(postModel.Postulantes.Select(newmodel => newmodel.ToEntity<Postulante, PostulanteModel>(_aplicacionMapper)));
            aplicacion.AddGarantias(postModel.Garantias.Select(newmodel => newmodel.ToEntity<Garantia, GarantiaModel>(_aplicacionMapper)));
            aplicacion = await _aplicacionRepository.AddAplicacion(aplicacion);

            return aplicacion.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper);
        }

        public async Task<AplicacionGetModel> UpdateActiveAplicacion(AplicacionPutModel putModel)
        {
            IReadOnlyCollection<Aplicacion> aplicaciones = await _aplicacionRepository
                .GetAplicaciones(putModel.InquilinoUsuarioId!.Value);

            Aplicacion? aplicacion = aplicaciones
                .FirstOrDefault(ap => ap.Status.IsActive()) ?? throw new NotFoundException();
            if (aplicacion.Status != AplicacionStatus.AprobacionPendiente)
                throw new BadRequestException("Aplicacion", "La aplicación no se encuentra en estado editable");

            putModel.PuntuacionTotal = 10;
            aplicacion = putModel.ToEntity(_aplicacionMapper, aplicacion);
            aplicacion = await UpsertPostulantes(putModel, aplicacion);
            aplicacion = await UpsertGarantias(putModel, aplicacion);
            aplicacion = await _aplicacionRepository.UpdateAplicacion(aplicacion);

            return aplicacion.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper);
        }

        public async Task<AplicacionGetModel> GetAplicacion(int aplicacionId)
        {
            Aplicacion aplicacion = await _aplicacionRepository.GetAplicacion(aplicacionId) ?? throw new NotFoundException();
            return aplicacion.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper);
        }

        public async Task<IReadOnlyCollection<AplicacionGetModel>> GetAplicaciones(int inquilinoUsuarioId)
        => (await _aplicacionRepository.GetAplicaciones(inquilinoUsuarioId))
                .Select(ap => ap.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper))
                .ToList();

        public async Task<IReadOnlyCollection<AplicacionGetModel>> GetAplicaciones()
        => (await _aplicacionRepository.GetAplicaciones())
                .Select(ap => ap.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper))
                .ToList();

        public async Task<AplicacionGetModel> AprobarAplicacion(int aplicacionId, AprobarAplicacionPutModel model)
        {
            Aplicacion? aplicacion = await _aplicacionRepository.GetAplicacion(aplicacionId) ?? throw new NotFoundException();
            if (aplicacion.Status != AplicacionStatus.AprobacionPendiente)
                throw new BadRequestException("Aplicacion", "La aplicación no se encuentra en estado para aprobar");
            if (!aplicacion.Postulantes.All(po => model.Puntuaciones.Count(pu => pu.PostulanteId == po.Id) == 1))
                throw new BadRequestException(nameof(model.Puntuaciones), "Los ids de las puntuaciones no coincide con los postulantes");
            if (!model.Puntuaciones.All(pu => pu.PuntuacionCrediticia >= 0 && pu.PuntuacionCrediticia <= 1000))
                throw new BadRequestException(nameof(PostulanteCalificacionModel.PuntuacionCrediticia), "La puntuación crediticia debe estar entre 0 y 1000");
            if (!model.Puntuaciones.All(pu => pu.PuntuacionPenal >= 0 && pu.PuntuacionPenal <= 1000))
                throw new BadRequestException(nameof(PostulanteCalificacionModel.PuntuacionPenal), "La puntuación penal debe estar entre 0 y 1000");
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            IndiceValor indiceValor = await _aplicacionRepository.GetIcl(today) 
                ?? throw new BadRequestException("ICL", "No hay indice de contratos de alquiler registrados");
            decimal icl = indiceValor.Valor;
            if(icl <= 0)
                throw new BadRequestException("ICL", "Indice de contratos de alquiler no puede ser negativo");
            aplicacion = model.ToEntity(_aplicacionMapper, aplicacion);
            aplicacion.PuntuacionTotal = CalculatePuntuacionTotal(aplicacion, icl);
            aplicacion = await _aplicacionRepository.UpdateAplicacion(aplicacion);
            Usuario inquilinoUsuario = aplicacion.InquilinoUsuario;

            await _aprobarAplicacionEmailSender.SendEmail(inquilinoUsuario.Email!,
                 new AprobarAplicacionEmailModel
                 {
                     Nombre = inquilinoUsuario.Nombre,
                     Apellido = inquilinoUsuario.Apellido
                 });

            return aplicacion.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper);
        }

        public async Task<AplicacionGetModel> RechazarAplicacion(int aplicacionId, RechazarAplicacionPutModel model)
        {
            Aplicacion? aplicacion = await _aplicacionRepository.GetAplicacion(aplicacionId) ?? throw new NotFoundException();
            if (aplicacion.Status != AplicacionStatus.AprobacionPendiente)
                throw new BadRequestException("Aplicacion", "La aplicación no se encuentra en estado para aprobar");

            aplicacion = model.ToEntity(_aplicacionMapper, aplicacion);
            aplicacion = await _aplicacionRepository.UpdateAplicacion(aplicacion);
            Usuario inquilinoUsuario = aplicacion.InquilinoUsuario;

            await _rechazarAplicacionEmailSender.SendEmail(inquilinoUsuario.Email!,
                 new RechazarAplicacionEmailModel
                 {
                     Nombre = inquilinoUsuario.Nombre,
                     Apellido = inquilinoUsuario.Apellido,
                     Comentarios = aplicacion.Comentarios.OrderByDescending(c => c.Fecha)
                     .Select(c =>
                     new ComentarioEmailModel
                     {
                         Fecha = $"{c.Fecha.ToShortDateString()} {c.Fecha.ToShortTimeString()}",
                         Comentario = c.Comentario
                     }).ToList()
                 });

            return aplicacion.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper);
        }
    }
}
