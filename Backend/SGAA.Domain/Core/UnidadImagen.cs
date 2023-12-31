﻿namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class UnidadImagen : BaseEntity, IEntity
    {
        public UnidadImagen(int unidadDetalleId, string titulo, string descripcion, string archivo)
        {
            UnidadDetalleId = unidadDetalleId;
            Titulo = titulo;
            Descripcion = descripcion;
            Archivo = archivo;
        }

        public int UnidadDetalleId { get; private set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Archivo { get; set; }

        public UnidadDetalle Detalle { get; set; } = default!;
    }
}
