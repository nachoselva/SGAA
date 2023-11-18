namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System;
    using System.Linq;

    public class UnidadDetalle : BaseEntity, IEntity
    {
        private readonly List<UnidadImagen> _imagenes;

        public UnidadDetalle(int unidadId, string descripcion, decimal superficie, int ambientes, int banios, int dormitorios, int cocheras)
        {
            UnidadId = unidadId;
            Descripcion = descripcion;
            Superficie = superficie;
            Ambientes = ambientes;
            Banios = banios;
            Dormitorios = dormitorios;
            Cocheras = cocheras;
            _imagenes = new List<UnidadImagen>();
        }

        public int UnidadId { get; set; }
        public string Descripcion { get; set; }
        public decimal Superficie { get; set; }
        public int Ambientes { get; set; }
        public int Banios { get; set; }
        public int Dormitorios { get; set; }
        public int Cocheras { get; set; }

        public Unidad Unidad { get; set; } = default!;
        public IReadOnlyCollection<UnidadImagen> Imagenes => _imagenes.AsReadOnly();

        public void AddImagenes(IEnumerable<UnidadImagen> imagenes)
        {
            _imagenes.AddRange(imagenes);
        }

        public void RemoveImagenes(IEnumerable<UnidadImagen> imagenes)
        {
            IEnumerable<int> idsToDelete = imagenes.Select(img => img.Id);
            _imagenes.RemoveAll(img => idsToDelete.Contains(img.Id));
        }
    }
}
