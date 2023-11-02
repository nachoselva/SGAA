namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class AplicacionMapper : IAplicacionMapper
    {
        public AplicacionGetModel ToGetModel(Aplicacion entity)
         => new()
         {
             Id = entity.Id,
             InquilinoUsuarioId = entity.InquilinoUsuarioId,
             InquilinoUsuarioNombreCompleto = entity.InquilinoUsuario.NombreCompleto,
             Status = entity.Status,
             PuntuacionTotal = entity.PuntuacionTotal,
             Garantias = entity.Garantias.Select(g => g.MapToGetModel<Garantia, GarantiaModel>(this)).ToList(),
             Postulantes = entity.Postulantes.Select(p => p.MapToGetModel<Postulante, PostulanteModel>(this)).ToList(),
             Comentarios = entity.Comentarios.Select(c => c.MapToGetModel<AplicacionComentario, ComentarioModel>(this)).ToList(),
             Postulaciones = entity.Postulaciones.Count
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
        => new()
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Apellido = entity.Apellido,
            Email = entity.Email,
            NumeroIdentificacion = entity.NumeroIdentificacion,
            FechaNacimiento = entity.FechaNacimiento,
            Domicilio = entity.Domicilio,
            FrenteIdentificacionArchivo = entity.FrenteIdentificacionArchivo,
            DorsoIdentificacionArchivo = entity.DorsoIdentificacionArchivo,
            AplicacionId = entity.AplicacionId,
            FechaEmpleadoDesde = entity.FechaEmpleadoDesde,
            NombreEmpresa = entity.NombreEmpresa,
            IngresoMensual = entity.IngresoMensual,
            ReciboDeSueldoArchivo = entity.ReciboDeSueldoArchivo
        };
        public Postulante ToEntity(PostulanteModel postModel)
        => new(postModel.Nombre, postModel.Apellido, postModel.Email, postModel.NumeroIdentificacion,
            postModel.FechaNacimiento, postModel.Domicilio,
            postModel.FrenteIdentificacionArchivo,
            postModel.DorsoIdentificacionArchivo,
            postModel.AplicacionId ?? 0, postModel.FechaEmpleadoDesde, postModel.NombreEmpresa, postModel.IngresoMensual,
            postModel.ReciboDeSueldoArchivo, null, null);
        public Postulante ToEntity(PostulanteModel putModel, Postulante entity)
        {
            entity.Nombre = putModel.Nombre;
            entity.Apellido = putModel.Apellido;
            entity.Email = putModel.Email;
            entity.NumeroIdentificacion = putModel.NumeroIdentificacion;
            entity.FechaNacimiento = putModel.FechaNacimiento;
            entity.Domicilio = putModel.Domicilio;
            entity.FrenteIdentificacionArchivo = putModel.FrenteIdentificacionArchivo;
            entity.DorsoIdentificacionArchivo = putModel.DorsoIdentificacionArchivo;
            entity.AplicacionId = putModel.AplicacionId!.Value;
            entity.FechaEmpleadoDesde = putModel.FechaEmpleadoDesde;
            entity.NombreEmpresa = putModel.NombreEmpresa;
            entity.IngresoMensual = putModel.IngresoMensual;
            entity.ReciboDeSueldoArchivo = putModel.ReciboDeSueldoArchivo;
            return entity;
        }

        public GarantiaModel ToGetModel(Garantia entity)
        => new()
        {
            Id = entity.Id,
            AplicacionId = entity.AplicacionId,
            Archivo = entity.Archivo,
            Monto = entity.Monto
        };
        public Garantia ToEntity(GarantiaModel postModel)
        => new(postModel.AplicacionId ?? 0, postModel.Monto,
            postModel.Archivo);
        public Garantia ToEntity(GarantiaModel putModel, Garantia entity)
        {
            entity.AplicacionId = putModel.AplicacionId!.Value;
            entity.Monto = putModel.Monto;
            entity.Archivo = putModel.Archivo;
            return entity;
        }

        public ComentarioModel ToGetModel(AplicacionComentario entity)
        => new()
        {
            Fecha = entity.Fecha,
            Comentario = entity.Comentario
        };

        public Aplicacion ToEntity(RechazarAplicacionPutModel putModel, Aplicacion entity)
        {
            entity.AddComentario(new AplicacionComentario(entity.Id, putModel.Comentario, DateTime.Now));
            return entity;
        }

        public Aplicacion ToEntity(AprobarAplicacionPutModel putModel, Aplicacion entity)
        {
            entity.Status = AplicacionStatus.Aprobada;
            foreach (var postulante in entity.Postulantes)
            {
                PostulanteCalificacionModel puntuacion = putModel.Puntuaciones.First(p => p.PostulanteId == postulante.Id);
                postulante.PuntuacionCrediticia = puntuacion.PuntuacionCrediticia;
                postulante.PuntuacionPenal = puntuacion.PuntuacionPenal;
            }
            return entity;
        }
    }
}
