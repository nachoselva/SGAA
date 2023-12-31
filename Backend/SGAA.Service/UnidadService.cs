﻿namespace SGAA.Service
{
    using SGAA.Domain.Core;
    using SGAA.Domain.Errors;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Models;
    using SGAA.Models.Base;
    using SGAA.Models.Mappers;
    using SGAA.Repository.Contracts;
    using SGAA.Service.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UnidadService : IUnidadService
    {
        private readonly IUnidadRepository _unidadRepository;
        private readonly IUnidadMapper _unidadMapper;
        private readonly IAprobarUnidadEmailSender _aprobarUnidadEmailSender;
        private readonly IRechazarUnidadEmailSender _rechazarUnidadEmailSender;

        public UnidadService(IUnidadRepository unidadRepository, IUnidadMapper unidadMapper, IAprobarUnidadEmailSender aprobarUnidadEmailSender,
            IRechazarUnidadEmailSender rechazarUnidadEmailSender)
        {
            _unidadRepository = unidadRepository;
            _unidadMapper = unidadMapper;
            _aprobarUnidadEmailSender = aprobarUnidadEmailSender;
            _rechazarUnidadEmailSender = rechazarUnidadEmailSender;
        }

        private async Task<Unidad> UpsertImagenes(UnidadPutModel putModel, Unidad unidad)
        {
            UnidadImagenModel[] updatedImageModels = putModel.Detalle.Imagenes.Where(img => img.Id.HasValue && img.Id.Value > 0).ToArray();
            UnidadImagenModel[] newImageModels = putModel.Detalle.Imagenes.Except(updatedImageModels).ToArray();
            int[] notDeletedIds = updatedImageModels.Where(img => img.Id.HasValue).Select(img => img.Id!.Value).ToArray();

            //Add imagenes

            unidad.Detalle.AddImagenes(newImageModels.Select(newmodel => newmodel.ToEntity<UnidadImagen, UnidadImagenModel>(_unidadMapper)));

            //Update imagenes

            foreach (var updateImageModel in updatedImageModels)
            {
                UnidadImagen imagen = unidad.Detalle.Imagenes.First(img => img.Id == updateImageModel.Id);
                imagen = updateImageModel.ToEntity(_unidadMapper, imagen);
            }

            //Delete imagenes

            UnidadImagen[] entitiesToDelete = unidad.Detalle.Imagenes
                .Where(entity => entity.Id > 0)
                .Where(entity => !notDeletedIds.Contains(entity.Id))
                .ToArray();

            unidad.Detalle.RemoveImagenes(entitiesToDelete);

            if (entitiesToDelete.Any())
                await _unidadRepository.DeleteImagenes(entitiesToDelete);

            return unidad;
        }

        private async Task<Unidad> UpsertTitulares(UnidadPutModel putModel, Unidad unidad)
        {
            TitularModel[] updatedImageModels = putModel.Titulares.Where(tit => tit.Id.HasValue && tit.Id.Value > 0).ToArray();
            TitularModel[] newImageModels = putModel.Titulares.Except(updatedImageModels).ToArray();
            int[] notDeletedIds = updatedImageModels.Where(tit => tit.Id.HasValue).Select(tit => tit.Id!.Value).ToArray();

            //Add titulares

            unidad.AddTitulares(newImageModels.Select(newmodel => newmodel.ToEntity<Titular, TitularModel>(_unidadMapper)));

            //Update titulares

            foreach (var updateImageModel in updatedImageModels)
            {
                Titular titular = unidad.Titulares.First(img => img.Id == updateImageModel.Id);
                titular = updateImageModel.ToEntity(_unidadMapper, titular);
            }

            //Delete titulares

            Titular[] entitiesToDelete = unidad.Titulares
                .Where(entity => entity.Id > 0)
                .Where(entity => !notDeletedIds.Contains(entity.Id))
                .ToArray();

            unidad.RemoveTitulares(entitiesToDelete);

            if (entitiesToDelete.Any())
                await _unidadRepository.DeleteTitulares(entitiesToDelete);

            return unidad;
        }

        public async Task<IReadOnlyCollection<UnidadGetModel>> GetUnidades(int propietarioUsuarioId)
        {
            IReadOnlyCollection<Unidad> unidades = await _unidadRepository.GetUnidades(propietarioUsuarioId);
            return unidades.Select(unidad => unidad.MapToGetModel<Unidad, UnidadGetModel>(_unidadMapper)).ToList();
        }

        public async Task<IReadOnlyCollection<UnidadGetModel>> GetUnidades()
        {
            IReadOnlyCollection<Unidad> unidades = await _unidadRepository.GetUnidades();
            return unidades.Select(unidad => unidad.MapToGetModel<Unidad, UnidadGetModel>(_unidadMapper)).ToList();
        }

        public async Task<UnidadGetModel> GetUnidad(int unidadId)
        {
            Unidad? unidad = await _unidadRepository.GetUnidad(unidadId);
            return unidad != null ? unidad.MapToGetModel<Unidad, UnidadGetModel>(_unidadMapper) : throw new NotFoundException();
        }

        public async Task<UnidadGetModel> GetUnidad(int propietarioUsuarioId, int unidadId)
        {
            Unidad? unidad = await _unidadRepository.GetUnidad(unidadId);
            return unidad != null && unidad.PropietarioUsuarioId == propietarioUsuarioId
                ? unidad.MapToGetModel<Unidad, UnidadGetModel>(_unidadMapper) : throw new NotFoundException();
        }

        public async Task<UnidadGetModel> AddUnidad(UnidadPostModel postModel)
        {
            if (!postModel.Titulares.Any())
                throw new BadRequestException("Unidad", "Debe haber por lo menos un titular");
            Unidad? unidadExistente = await _unidadRepository.GetUnidad(postModel.CiudadId, postModel.Calle, postModel.Altura, postModel.Piso, postModel.Departamento);
            if (unidadExistente != null)
                throw new BadRequestException(nameof(postModel.Calle), "Existe una unidad registrada en el mismo domicilio.");
            Propiedad? propiedad = await _unidadRepository.GetPropiedad(postModel.CiudadId, postModel.Calle, postModel.Altura);
            if (propiedad != null)
                postModel.PropiedadId = propiedad.Id;
            Unidad unidad = postModel.ToEntity<Unidad, UnidadPostModel>(_unidadMapper);
            unidad.Detalle = postModel.Detalle.ToEntity<UnidadDetalle, UnidadDetalleModel>(_unidadMapper);
            unidad.Detalle.AddImagenes(postModel.Detalle.Imagenes.Select(newmodel => newmodel.ToEntity<UnidadImagen, UnidadImagenModel>(_unidadMapper)));
            unidad.AddTitulares(postModel.Titulares.Select(newmodel => newmodel.ToEntity<Titular, TitularModel>(_unidadMapper)));
            unidad = await _unidadRepository.AddUnidad(unidad);
            unidad = (await _unidadRepository.GetUnidad(unidad.Id))!;
            return unidad.MapToGetModel<Unidad, UnidadGetModel>(_unidadMapper);
        }

        public async Task<UnidadGetModel> UpdateUnidad(int unidadId, UnidadPutModel putModel)
        {
            if (!putModel.Titulares.Any())
                throw new BadRequestException("Unidad", "Debe haber por lo menos un titular");
            Unidad? unidadExistente = await _unidadRepository.GetUnidad(putModel.CiudadId, putModel.Calle, putModel.Altura, putModel.Piso, putModel.Departamento);
            if (unidadExistente != null && unidadExistente.Id != unidadId)
                throw new BadRequestException(nameof(putModel.Calle), "Existe una unidad registrada en el mismo domicilio.");
            Unidad? unidad = await _unidadRepository.GetUnidad(unidadId);
            if (unidad == null || putModel.PropietarioUsuarioId != unidad.PropietarioUsuarioId)
                throw new NotFoundException();
            if (unidad.Status != UnidadStatus.AprobacionPendiente)
                throw new BadRequestException(nameof(unidad.Status), "La unidad no se encuentrá en estado editable");
            Propiedad? propiedad = await _unidadRepository.GetPropiedad(putModel.CiudadId, putModel.Calle, putModel.Altura);
            if (propiedad != null)
                putModel.PropiedadId = propiedad.Id;

            unidad = putModel.ToEntity(_unidadMapper, unidad);
            unidad.Detalle = putModel.Detalle.ToEntity(_unidadMapper, unidad.Detalle);
            unidad = await UpsertImagenes(putModel, unidad);
            unidad = await UpsertTitulares(putModel, unidad);
            await _unidadRepository.UpdateUnidad(unidad);
            unidad = (await _unidadRepository.GetUnidad(unidadId))!;
            return unidad.MapToGetModel<Unidad, UnidadGetModel>(_unidadMapper);
        }

        public async Task<UnidadGetModel> AprobarUnidad(int unidadId, AprobarUnidadPutModel model)
        {
            Unidad? unidad = await _unidadRepository.GetUnidad(unidadId) ?? throw new NotFoundException();
            if (unidad.Status != UnidadStatus.AprobacionPendiente)
                throw new BadRequestException(nameof(unidad.Status), "La unidad no se encuentrá en estado para aprobar");
            unidad = model.ToEntity(_unidadMapper, unidad);
            unidad = await _unidadRepository.UpdateUnidad(unidad);

            await _aprobarUnidadEmailSender.SendEmail(unidad.PropietarioUsuario.Email!,
                 new AprobarUnidadEmailModel
                 {
                     Nombre = unidad.PropietarioUsuario.Nombre,
                     Apellido = unidad.PropietarioUsuario.Apellido,
                     Domicilio = unidad.DomicilioCompleto
                 });

            return unidad.MapToGetModel<Unidad, UnidadGetModel>(_unidadMapper);
        }

        public async Task<UnidadGetModel> RechazarUnidad(int unidadId, RechazarUnidadPutModel model)
        {
            Unidad? unidad = await _unidadRepository.GetUnidad(unidadId) ?? throw new NotFoundException();
            if (unidad.Status != UnidadStatus.AprobacionPendiente)
                throw new BadRequestException(nameof(unidad.Status), "La unidad no se encuentrá en estado para aprobar");
            unidad = model.ToEntity(_unidadMapper, unidad);
            unidad = await _unidadRepository.UpdateUnidad(unidad);

            await _rechazarUnidadEmailSender.SendEmail(unidad.PropietarioUsuario.Email!,
                 new RechazarUnidadEmailModel
                 {
                     Nombre = unidad.PropietarioUsuario.Nombre,
                     Apellido = unidad.PropietarioUsuario.Apellido,
                     Domicilio = unidad.DomicilioCompleto,
                     Comentarios = unidad.Comentarios.OrderByDescending(c => c.Fecha)
                     .Select(c =>
                     new ComentarioEmailModel
                     {
                         Fecha = $"{c.Fecha.ToShortDateString()} {c.Fecha.ToShortTimeString()}",
                         Comentario = c.Comentario
                     }).ToList()
                 });

            return unidad.MapToGetModel<Unidad, UnidadGetModel>(_unidadMapper);
        }
    }
}
