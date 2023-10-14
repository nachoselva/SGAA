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
                Comentarios = entity.Comentarios.Select(c =>
                new UnidadComentarioModel()
                {
                    Fecha = c.Fecha,
                    Comentario = c.Comentario
                }).ToList(),
                Detalle = new UnidadDetalleModel
                {
                    Id = entity.Detalle.Id,
                    UnidadId = entity.Id,
                    Descripcion = entity.Detalle.Descripcion,
                    Superficie = entity.Detalle.Superficie,
                    Ambientes = entity.Detalle.Ambientes,
                    Banios = entity.Detalle.Banios,
                    Dormitorios = entity.Detalle.Dormitorios,
                    Cocheras = entity.Detalle.Cocheras,
                    Imagenes = entity.Detalle.Imagenes.Select(imagen =>
                    new UnidadImagenModel()
                    {
                        Id = imagen.Id,
                        UnidadDetalleId = imagen.UnidadDetalleId,
                        Titulo = imagen.Titulo,
                        Descripcion = imagen.Descripcion,
                        Archivo = Encoding.ASCII.GetString(imagen.Archivo)
                    }).ToList()
                }
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
    }
}
