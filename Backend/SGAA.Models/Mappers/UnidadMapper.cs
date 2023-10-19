namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using System.Text;

    public class UnidadMapper : IUnidadMapper
    {
        public UnidadGetModel ToGetModel(Unidad entity)
        {
            return new UnidadGetModel
            {
                Id = entity.Id,
                PropiedadId = entity.PropiedadId,
                PropietarioUsuarioId = entity.PropietarioUsuarioId,
                CiudadId = entity.Propiedad.CiudadId,
                Calle = entity.Propiedad.Calle,
                Altura = entity.Propiedad.Altura,
                Piso = entity.Piso,
                Departamento = entity.Departamento,
                FechaAdquisicion = entity.FechaAdquisicion,
                TituloPropiedadArchivo = Encoding.ASCII.GetString(entity.TituloPropiedadArchivo),
                Status = entity.Status,
                Ciudad = entity.Propiedad.Ciudad.Nombre,
                Provincia = entity.Propiedad.Ciudad.Provincia.Nombre,
                DomicilioCompleto = entity.DomicilioCompleto,
                Comentarios = entity.Comentarios.Select(comentario => comentario.MapToGetModel<UnidadComentario, ComentarioModel>(this)).ToList(),
                Detalle = entity.Detalle.MapToGetModel<UnidadDetalle, UnidadDetalleModel>(this),
                Titulares = entity.Titulares.Select(titular => titular.MapToGetModel<Titular, TitularModel>(this)).ToList()
            };
        }

        public Unidad ToEntity(UnidadPostModel postModel)
        {
            int propiedadId = 0;
            Propiedad? propiedad = null;
            if (postModel.PropiedadId.HasValue)
            {
                propiedadId = postModel.PropiedadId.Value;
            }
            else
            {
                propiedad = new Propiedad(postModel.CiudadId, postModel.Calle, postModel.Altura);
            }

            Unidad unidad = new(
                propiedadId,
                postModel.PropietarioUsuarioId!.Value,
                postModel.Piso,
                postModel.Departamento,
                postModel.FechaAdquisicion,
                Encoding.ASCII.GetBytes(postModel.TituloPropiedadArchivo),
                UnidadStatus.AprobacionPendiente);

            if (propiedad != null)
                unidad.Propiedad = propiedad;

            return unidad;
        }

        public Unidad ToEntity(UnidadPutModel putModel, Unidad entity)
        {
            if (putModel.PropiedadId.HasValue)
            {
                entity.PropiedadId = putModel.PropiedadId.Value;
            }
            else
            {
                entity.Propiedad = new Propiedad(putModel.CiudadId, putModel.Calle, putModel.Altura);
            }
            entity.Piso = putModel.Piso;
            entity.Departamento = putModel.Departamento;
            entity.TituloPropiedadArchivo = Encoding.ASCII.GetBytes(putModel.TituloPropiedadArchivo);
            entity.Piso = putModel.Piso;

            return entity;
        }

        public UnidadDetalle ToEntity(UnidadDetalleModel postModel)
        {
            return new UnidadDetalle(
                postModel.UnidadId ?? 0,
                postModel.Descripcion,
                postModel.Superficie,
                postModel.Ambientes,
                postModel.Banios,
                postModel.Dormitorios,
                postModel.Cocheras);
        }

        public UnidadDetalle ToEntity(UnidadDetalleModel putModel, UnidadDetalle entity)
        {
            entity.Descripcion = putModel.Descripcion;
            entity.Superficie = putModel.Superficie;
            entity.Ambientes = putModel.Ambientes;
            entity.Banios = putModel.Banios;
            entity.Dormitorios = putModel.Dormitorios;
            entity.Cocheras = putModel.Cocheras;
            return entity;
        }

        public UnidadImagen ToEntity(UnidadImagenModel postModel)
        {
            return new UnidadImagen(postModel.UnidadDetalleId ?? 0, postModel.Titulo, postModel.Descripcion, Encoding.ASCII.GetBytes(postModel.Archivo));
        }

        public UnidadImagen ToEntity(UnidadImagenModel putModel, UnidadImagen entity)
        {
            entity.Titulo = putModel.Titulo;
            entity.Descripcion = putModel.Descripcion;
            entity.Archivo = Encoding.ASCII.GetBytes(putModel.Archivo);
            return entity;
        }

        public Unidad ToEntity(AprobarUnidadPutModel putModel, Unidad entity)
        {
            entity.Status = UnidadStatus.DocumentacionAprobada;
            return entity;
        }

        public Unidad ToEntity(RechazarUnidadPutModel putModel, Unidad entity)
        {
            entity.AddComentario(new UnidadComentario(entity.Id, putModel.Comentario, DateTime.Now));
            return entity;
        }

        public Titular ToEntity(TitularModel putModel, Titular entity)
        {
            entity.UnidadId = putModel.UnidadId ?? 0;
            entity.Nombre = putModel.Nombre;
            entity.Apellido = putModel.Apellido;
            entity.Email = putModel.Email;
            entity.TipoIdentificacion = putModel.TipoIdentificacion;
            entity.NumeroIdentificacion = putModel.NumeroIdentificacion;
            entity.FechaNacimiento = putModel.FechaNacimiento;
            entity.Domicilio = putModel.Domicilio;
            entity.FrenteIdentificacionArchivo = Encoding.ASCII.GetBytes(putModel.FrenteIdentificacionArchivo);
            entity.DorsoIdentificacionArchivo = Encoding.ASCII.GetBytes(putModel.DorsoIdentificacionArchivo);
            return entity;
        }

        public Titular ToEntity(TitularModel postModel)
        {
            return new Titular(
                postModel.UnidadId ?? 0,
                postModel.Nombre,
                postModel.Apellido,
                postModel.Email,
                postModel.TipoIdentificacion,
                postModel.NumeroIdentificacion,
                postModel.FechaNacimiento,
                postModel.Domicilio,
                Encoding.ASCII.GetBytes(postModel.FrenteIdentificacionArchivo),
                Encoding.ASCII.GetBytes(postModel.DorsoIdentificacionArchivo)
                );
        }

        public TitularModel ToGetModel(Titular entity)
        =>
            new()
            {
                Id = entity.Id,
                UnidadId = entity.UnidadId,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Email = entity.Email,
                TipoIdentificacion = entity.TipoIdentificacion,
                NumeroIdentificacion = entity.NumeroIdentificacion,
                FechaNacimiento = entity.FechaNacimiento,
                Domicilio = entity.Domicilio,
                FrenteIdentificacionArchivo = Encoding.ASCII.GetString(entity.FrenteIdentificacionArchivo),
                DorsoIdentificacionArchivo = Encoding.ASCII.GetString(entity.DorsoIdentificacionArchivo)
            };

        public ComentarioModel ToGetModel(UnidadComentario entity)
        => new()
        {
            Fecha = entity.Fecha,
            Comentario = entity.Comentario
        };

        public UnidadDetalleModel ToGetModel(UnidadDetalle entity)
        => new()
        {
            Id = entity.Id,
            UnidadId = entity.Id,
            Descripcion = entity.Descripcion,
            Superficie = entity.Superficie,
            Ambientes = entity.Ambientes,
            Banios = entity.Banios,
            Dormitorios = entity.Dormitorios,
            Cocheras = entity.Cocheras,
            Imagenes = entity.Imagenes.Select(imagen => imagen.MapToGetModel<UnidadImagen, UnidadImagenModel>(this)).ToList()
        };

        public UnidadImagenModel ToGetModel(UnidadImagen entity)
        =>
            new()
            {
                Id = entity.Id,
                UnidadDetalleId = entity.UnidadDetalleId,
                Titulo = entity.Titulo,
                Descripcion = entity.Descripcion,
                Archivo = Encoding.ASCII.GetString(entity.Archivo)
            };
    }
}
