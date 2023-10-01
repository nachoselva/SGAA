namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class UnidadDetalle : BaseEntity, IEntity
    {
        public UnidadDetalle(string descripcion, decimal superficie, int ambientes, int banios, int dormitorios, int cocheras)
        {
            Descripcion = descripcion;
            Superficie = superficie;
            Ambientes = ambientes;
            Banios = banios;
            Dormitorios = dormitorios;
            Cocheras = cocheras;
        }

        public string Descripcion { get; private set; }
        public decimal Superficie { get; private set; }
        public int Ambientes { get; private set; }
        public int Banios { get; private set; }
        public int Dormitorios { get; private set; }
        public int Cocheras { get; private set; }

        public IReadOnlyCollection<UnidadImagen> Imagenes { get; private set; } = Array.Empty<UnidadImagen>();
    }
}
