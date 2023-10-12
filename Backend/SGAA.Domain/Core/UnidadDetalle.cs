namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class UnidadDetalle : BaseEntity, IEntity
    {
        private List<UnidadImagen> _imagenes;

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
        public string Descripcion { get; private set; }
        public decimal Superficie { get; private set; }
        public int Ambientes { get; private set; }
        public int Banios { get; private set; }
        public int Dormitorios { get; private set; }
        public int Cocheras { get; private set; }

        public Unidad Unidad { get; set; } = default!;
        public IReadOnlyCollection<UnidadImagen> Imagenes => _imagenes.AsReadOnly();

        public void AddImagenes(IEnumerable<UnidadImagen> imagen)
        {
            _imagenes.AddRange(imagen);
        }
    }
}
