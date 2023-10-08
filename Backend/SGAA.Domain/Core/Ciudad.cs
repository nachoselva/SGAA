namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class Ciudad : BaseEntity, IEntity
    {
        public Ciudad(int id, int provinciaId, string nombre, string nombreCompleto)
        {
            Id = id;
            ProvinciaId = provinciaId;
            Nombre = nombre;
            NombreCompleto = nombreCompleto;
        }
        public int ProvinciaId { get; private set; }
        public string Nombre { get; private set; }
        public string NombreCompleto { get; private set; }

        public Provincia Provincia { get; } = default!;
        public IReadOnlyCollection<Propiedad> Propiedades { get; } = Array.Empty<Propiedad>();
    }
}
