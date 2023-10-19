﻿namespace SGAA.Service
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
    using SGAA.Utils;
    using SGAA.Utils.Configuration;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PublicacionService : IPublicacionService
    {
        private readonly ISGAAConfiguration _configuration;
        private readonly IPublicacionRepository _publicacionRepository;
        private readonly IPublicacionMapper _publicacionMapper;
        private readonly IUnidadRepository _unidadRepository;
        private readonly IPublicarUnidadEmailSender _publicarUnidadEmailSender;
        private readonly ICancelarPostulacionEmailSender _cancelarPostulacionEmailSender;
        private readonly IReservaOfrecidaInquilinoEmailSender _reservaOfrecidaInquilinoEmailSender;
        private readonly IReservaOfrecidaPropietarioEmailSender _reservaOfrecidaPropietarioEmailSender;

        public PublicacionService(ISGAAConfiguration configuration, IPublicacionRepository publicacionRepository,
            IPublicacionMapper publicacionMapper, IUnidadRepository unidadRepository, IPublicarUnidadEmailSender publicarUnidadEmailSender,
            ICancelarPostulacionEmailSender cancelarPostulacionEmailSender, IReservaOfrecidaInquilinoEmailSender reservaOfrecidaInquilinoEmailSender,
            IReservaOfrecidaPropietarioEmailSender reservaOfrecidaPropietarioEmailSender)
        {
            _configuration = configuration;
            _publicacionRepository = publicacionRepository;
            _publicacionMapper = publicacionMapper;
            _unidadRepository = unidadRepository;
            _publicarUnidadEmailSender = publicarUnidadEmailSender;
            _cancelarPostulacionEmailSender = cancelarPostulacionEmailSender;
            _reservaOfrecidaInquilinoEmailSender = reservaOfrecidaInquilinoEmailSender;
            _reservaOfrecidaPropietarioEmailSender = reservaOfrecidaPropietarioEmailSender;
        }

        public async Task<PublicacionGetModel> GetPublicacionActiveByCodigo(string codigo)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacionByCodigo(codigo);
            return publicacion != null && publicacion.Status.IsActive() ?
                publicacion.MapToGetModel(_publicacionMapper) :
                throw new NotFoundException();
        }

        public async Task<PublicacionGetModel> GetPublicacionByPublicacionId(int publicacionId)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacionById(publicacionId);
            return publicacion != null ? publicacion.MapToGetModel(_publicacionMapper) : throw new NotFoundException();
        }

        public async Task<IReadOnlyCollection<PublicacionGetModel>> GetPublicacionesAdmin()
        {
            IReadOnlyCollection<Publicacion> publicaciones = await _publicacionRepository.GetPublicaciones();
            return publicaciones.Select(publicacion => publicacion.MapToGetModel(_publicacionMapper)).ToList();
        }

        public async Task<PublicacionGetModel> AddPublicacion(PublicacionPostModel model)
        {
            Unidad? unidad = await _unidadRepository.GetUnidadById(model.UnidadId);
            if (unidad == null || model.PropietarioUsuarioId != unidad.PropietarioUsuarioId)
                throw new NotFoundException();
            if (unidad.Status != UnidadStatus.DocumentacionAprobada)
                throw new BadRequestException(nameof(unidad.Status), "La unidad no tiene su documentación aprobada");
            if (unidad.Publicaciones.Any(p => p.Status.IsActive()))
                throw new BadRequestException(nameof(Publicacion.Status), "La unidad ya tiene una publicación activa");
            model.Codigo = StringExtensions.GenerateRandomString(30);
            Publicacion publicacion = model.ToEntity(_publicacionMapper);
            publicacion = await _publicacionRepository.AddPublicacion(publicacion);

            string publicacionURL = $"{_configuration.Frontend.Url}/Publicacion/{publicacion.Codigo}";

            await _publicarUnidadEmailSender.SendEmail(unidad.PropietarioUsuario.Email!,
                 new PublicarUnidadEmailModel
                 {
                     Nombre = unidad.PropietarioUsuario.Nombre,
                     Apellido = unidad.PropietarioUsuario.Apellido,
                     Domicilio = unidad.DomicilioCompleto,
                     InicioAlquiler = publicacion.InicioAlquiler.ToShortDateString(),
                     MontoAlquiler = publicacion.MontoAlquiler,
                     PublicacionURL = publicacionURL
                 });

            return publicacion.MapToGetModel(_publicacionMapper);
        }

        public async Task<PublicacionGetModel> CancelPublicacion(int publicacionId, PublicacionCancelarPutModel model)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacionById(publicacionId);
            if (publicacion == null || model.PropietarioUsuarioId != publicacion.Unidad.PropietarioUsuarioId)
                throw new NotFoundException();
            if (publicacion.Status != PublicacionStatus.Publicada)
                throw new BadRequestException(nameof(publicacion.Status), "La publicación no se encuentra en estado para cancelar");
            publicacion = model.ToEntity(_publicacionMapper, publicacion);
            foreach (var postulacion in publicacion.Postulaciones.Where(p => p.Status == PostulacionStatus.Postulada))
            {
                model.ToEntity(_publicacionMapper, postulacion);
            }
            List<Usuario> usuariosToBeNotified = new();
            foreach (var postulacion in publicacion.Postulaciones
                .Where(p => p.Status == PostulacionStatus.Postulada && p.Aplicacion.Status == AplicacionStatus.Aprobada))
            {
                postulacion.Status = PostulacionStatus.PublicacionCancelada;
                usuariosToBeNotified.Add(postulacion.Aplicacion.InquilinoUsuario);
            }
            publicacion = await _publicacionRepository.UpdatePublicacion(publicacion);
            foreach (var usuario in usuariosToBeNotified)
            {
                await _cancelarPostulacionEmailSender.SendEmail(usuario.Email!,
                    new CancelarPostulacionEmailModel()
                    {
                        Nombre = usuario.Nombre,
                        Apellido = usuario.Apellido,
                        Domicilio = publicacion.Unidad.DomicilioCompleto,
                        IsPropietarioAction = true
                    });
            }
            return publicacion.MapToGetModel(_publicacionMapper);
        }

        public async Task<PublicacionGetModel> CerrarPublicacion(int publicacionId, PublicacionCerrarPutModel model)
        {
            Publicacion? publicacion = await _publicacionRepository.GetPublicacionById(publicacionId);
            if (publicacion == null || model.PropietarioUsuarioId != publicacion.Unidad.PropietarioUsuarioId)
                throw new NotFoundException();
            if (publicacion.Status != PublicacionStatus.Publicada)
                throw new BadRequestException(nameof(publicacion.Status), "La publicación no se encuentra en estado para cerrar");
            var postulaciones = publicacion.Postulaciones
                .Where(p => p.Status == PostulacionStatus.Postulada)
                .Where(p => p.Aplicacion.Status == AplicacionStatus.Aprobada);
            var postulacionSelected = postulaciones
                .OrderByDescending(p => p.Aplicacion.PuntuacionTotal)
                .FirstOrDefault()
                ?? throw new BadRequestException(nameof(publicacion.Postulaciones), "La publicación no tiene postulaciones");
            publicacion = model.ToEntity(_publicacionMapper, publicacion);
            postulacionSelected = model.ToEntity(_publicacionMapper, postulacionSelected);
            publicacion = await _publicacionRepository.UpdatePublicacion(publicacion);

            Usuario propietario = publicacion.Unidad.PropietarioUsuario;
            Usuario inquilino = postulacionSelected.Aplicacion.InquilinoUsuario;

            await _reservaOfrecidaPropietarioEmailSender.SendEmail(propietario.Email!,
                   new ReservaOfrecidaPropietarioEmailModel()
                   {
                       Nombre = propietario.Nombre,
                       Apellido = propietario.Apellido,
                       Domicilio = publicacion.Unidad.DomicilioCompleto
                   });

            await _reservaOfrecidaInquilinoEmailSender.SendEmail(inquilino.Email!,
                    new ReservaOfrecidaInquilinoEmailModel()
                    {
                        Nombre = inquilino.Nombre,
                        Apellido = inquilino.Apellido,
                        Domicilio = publicacion.Unidad.DomicilioCompleto
                    });

            return publicacion.MapToGetModel(_publicacionMapper);
        }
    }
}
