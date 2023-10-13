namespace SGAA.Models.Mappers
{
    using SGAA.Domain.Core;

    public class UnidadDetalleMapper : IUnidadDetalleMapper
    {
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

    }
}
