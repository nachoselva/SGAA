﻿namespace SGAA.Service
{
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
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

        public AplicacionService(IAplicacionRepository aplicacionRepository, IAplicacionMapper aplicacionMapper)
        {
            _aplicacionRepository = aplicacionRepository;
            _aplicacionMapper = aplicacionMapper;
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

        public async Task<AplicacionGetModel?> GetActiveAplicacion(int inquilinoUsuarioId)
        {
            IReadOnlyCollection<Aplicacion> aplicaciones = await _aplicacionRepository
                .GetAplicacionesByInquilinoUsuarioId(inquilinoUsuarioId);

            Aplicacion? aplicacion = aplicaciones
                .FirstOrDefault(ap => ap.Status.IsActive());
            if (aplicacion == null || aplicacion.Status != AplicacionStatus.AprobacionPendiente)
                return null;
            return aplicacion.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper);
        }

        public async Task<AplicacionGetModel> AddAplicacion(AplicacionPostModel postModel)
        {
            IReadOnlyCollection<Aplicacion> aplicaciones = await _aplicacionRepository
                .GetAplicacionesByInquilinoUsuarioId(postModel.InquilinoUsuarioId!.Value);

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
                .GetAplicacionesByInquilinoUsuarioId(putModel.InquilinoUsuarioId!.Value);

            Aplicacion? aplicacion = aplicaciones
                .FirstOrDefault(ap => ap.Status.IsActive());
            if (aplicacion == null)
                throw new BadRequestException("Aplicacion", "No existe una aplicación active");
            if (aplicacion.Status != AplicacionStatus.AprobacionPendiente)
                throw new BadRequestException("Aplicacion", "La aplicación no se encuentra en estado editable");

            putModel.PuntuacionTotal = 10;
            aplicacion = putModel.ToEntity(_aplicacionMapper, aplicacion);
            aplicacion = await UpsertPostulantes(putModel, aplicacion);
            aplicacion = await UpsertGarantias(putModel, aplicacion);
            aplicacion = await _aplicacionRepository.UpdateAplicacion(aplicacion);

            return aplicacion.MapToGetModel<Aplicacion, AplicacionGetModel>(_aplicacionMapper);
        }
    }
}