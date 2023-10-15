﻿namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using System.Text;

    public class AplicacionMapper : IAplicacionMapper
    {
        public AplicacionGetModel ToGetModel(Aplicacion entity)
         => new AplicacionGetModel
         {
             Id = entity.Id,
             InquilinoUsuarioId = entity.InquilinoUsuarioId,
             Status = entity.Status,
             PuntuacionTotal = entity.PuntuacionTotal,
             Garantias = entity.Garantias.Select(g => g.MapToGetModel<Garantia, GarantiaModel>(this)).ToList(),
             Postulantes = entity.Postulantes.Select(p => p.MapToGetModel<Postulante, PostulanteModel>(this)).ToList(),
             Comentarios = entity.Comentarios.Select(c => c.MapToGetModel<AplicacionComentario, ComentarioModel>(this)).ToList()
         };
        public Aplicacion ToEntity(AplicacionPostModel postModel)
        => new(postModel.InquilinoUsuarioId!.Value, AplicacionStatus.AprobacionPendiente, 0);
        public Aplicacion ToEntity(AplicacionPutModel putModel, Aplicacion entity)
        {
            entity.InquilinoUsuarioId = putModel.InquilinoUsuarioId!.Value;
            entity.PuntuacionTotal = putModel.PuntuacionTotal!.Value;
            return entity;
        }

        public PostulanteModel ToGetModel(Postulante entity)
        => new PostulanteModel
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Apellido = entity.Apellido,
            TipoIdentificacion = entity.TipoIdentificacion,
            NumeroIdentificacion = entity.NumeroIdentificacion,
            FechaNacimiento = entity.FechaNacimiento,
            Domicilio = entity.Domicilio,
            FrenteIdentificacionArchivo = Encoding.ASCII.GetString(entity.FrenteIdentificacionArchivo),
            DorsoIdentificacionArchivo = Encoding.ASCII.GetString(entity.DorsoIdentificacionArchivo),
            AplicacionId = entity.AplicacionId,
            FechaEmpleadoDesde = entity.FechaEmpleadoDesde,
            NombreEmpresa = entity.NombreEmpresa,
            IngresoMensual = entity.IngresoMensual,
            ReciboDeSueldoArchivo = Encoding.ASCII.GetString(entity.ReciboDeSueldoArchivo)
        };
        public Postulante ToEntity(PostulanteModel postModel)
        => new(postModel.Nombre, postModel.Apellido, postModel.TipoIdentificacion, postModel.NumeroIdentificacion,
            postModel.FechaNacimiento, postModel.Domicilio,
            Encoding.ASCII.GetBytes(postModel.FrenteIdentificacionArchivo),
            Encoding.ASCII.GetBytes(postModel.DorsoIdentificacionArchivo),
            postModel.AplicacionId ?? 0, postModel.FechaEmpleadoDesde, postModel.NombreEmpresa, postModel.IngresoMensual,
            Encoding.ASCII.GetBytes(postModel.ReciboDeSueldoArchivo));
        public Postulante ToEntity(PostulanteModel putModel, Postulante entity)
        {
            entity.Nombre = putModel.Nombre;
            entity.Apellido = putModel.Apellido;
            entity.TipoIdentificacion = putModel.TipoIdentificacion;
            entity.NumeroIdentificacion = putModel.NumeroIdentificacion;
            entity.FechaNacimiento = putModel.FechaNacimiento;
            entity.Domicilio = putModel.Domicilio;
            entity.FrenteIdentificacionArchivo = Encoding.ASCII.GetBytes(putModel.FrenteIdentificacionArchivo);
            entity.DorsoIdentificacionArchivo = Encoding.ASCII.GetBytes(putModel.DorsoIdentificacionArchivo);
            entity.AplicacionId = putModel.AplicacionId!.Value;
            entity.FechaEmpleadoDesde = putModel.FechaEmpleadoDesde;
            entity.NombreEmpresa = putModel.NombreEmpresa;
            entity.IngresoMensual = putModel.IngresoMensual;
            entity.ReciboDeSueldoArchivo = Encoding.ASCII.GetBytes(putModel.ReciboDeSueldoArchivo);
            return entity;
        }

        public GarantiaModel ToGetModel(Garantia entity)
        => new GarantiaModel
        {
            Id = entity.Id,
            AplicacionId = entity.AplicacionId,
            Archivo = Encoding.ASCII.GetString(entity.Archivo),
            Monto = entity.Monto
        };
        public Garantia ToEntity(GarantiaModel postModel)
        => new Garantia(postModel.AplicacionId ?? 0, postModel.Monto,
            Encoding.ASCII.GetBytes(postModel.Archivo));
        public Garantia ToEntity(GarantiaModel putModel, Garantia entity)
        {
            entity.AplicacionId = putModel.AplicacionId!.Value;
            entity.Monto = putModel.Monto;
            entity.Archivo = Encoding.ASCII.GetBytes(putModel.Archivo);
            return entity;
        }

        public ComentarioModel ToGetModel(AplicacionComentario entity)
        => new ComentarioModel()
        {
            Fecha = entity.Fecha,
            Comentario = entity.Comentario
        };
    }
}
