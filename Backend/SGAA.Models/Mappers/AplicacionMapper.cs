namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using System.Text;

    public class AplicacionMapper : IAplicacionMapper
    {
        public AplicacionGetModel ToGetModel(Aplicacion entity)
         => new()
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
        => new()
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Apellido = entity.Apellido,
            Email = entity.Email,
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
        => new(postModel.Nombre, postModel.Apellido, postModel.Email, postModel.TipoIdentificacion, postModel.NumeroIdentificacion,
            postModel.FechaNacimiento, postModel.Domicilio,
            Encoding.ASCII.GetBytes(postModel.FrenteIdentificacionArchivo),
            Encoding.ASCII.GetBytes(postModel.DorsoIdentificacionArchivo),
            postModel.AplicacionId ?? 0, postModel.FechaEmpleadoDesde, postModel.NombreEmpresa, postModel.IngresoMensual,
            Encoding.ASCII.GetBytes(postModel.ReciboDeSueldoArchivo), null, null);
        public Postulante ToEntity(PostulanteModel putModel, Postulante entity)
        {
            entity.Nombre = putModel.Nombre;
            entity.Apellido = putModel.Apellido;
            entity.Email = putModel.Email;
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
        => new()
        {
            Id = entity.Id,
            AplicacionId = entity.AplicacionId,
            Archivo = Encoding.ASCII.GetString(entity.Archivo),
            Monto = entity.Monto
        };
        public Garantia ToEntity(GarantiaModel postModel)
        => new(postModel.AplicacionId ?? 0, postModel.Monto,
            Encoding.ASCII.GetBytes(postModel.Archivo));
        public Garantia ToEntity(GarantiaModel putModel, Garantia entity)
        {
            entity.AplicacionId = putModel.AplicacionId!.Value;
            entity.Monto = putModel.Monto;
            entity.Archivo = Encoding.ASCII.GetBytes(putModel.Archivo);
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
