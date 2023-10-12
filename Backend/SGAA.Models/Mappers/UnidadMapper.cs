namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;
    using System.Text;

    public class UnidadMapper : IUnidadMapper
    {
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

            Unidad unidad = new Unidad(
                propiedadId,
                postModel.PropietarioUsuarioId,
                postModel.Piso,
                postModel.Departamento,
                postModel.FechaAdquisicion,
                Encoding.ASCII.GetBytes(postModel.TituloPropiedadArchivo),
                UnidadStatus.AprobacionPendiente);

            UnidadDetalle detalle = new UnidadDetalle(
                unidad.Id,
                postModel.Descripcion,
                postModel.Superficie,
                postModel.Ambientes,
                postModel.Banios,
                postModel.Dormitorios,
                postModel.Cocheras);

            IEnumerable<UnidadImagen> imagenes =
                postModel.Imagenes.Select(i =>
                    new UnidadImagen(detalle.Id, i.Titulo, i.Descripcion, Encoding.ASCII.GetBytes(i.Archivo))
                );

            detalle.AddImagenes(imagenes);
            unidad.Detalle = detalle;
            if (propiedad != null)
                unidad.Propiedad = propiedad;

            return unidad;
        }

        public UnidadGetModel ToGetModel(Unidad entity)
        {
            return new UnidadGetModel { Id = entity.Id };
        }
    }
}
