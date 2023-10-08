namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class Provincia : BaseEntity, IEntity
    {
        public Provincia(int id, string nombre, string nombreCompleto)
        {
            Id = id;
            Nombre = nombre;
            NombreCompleto = nombreCompleto;
        }

        public string Nombre { get; private set; }
        public string NombreCompleto { get; private set; }

        public IReadOnlyCollection<Ciudad> Ciudades { get; private set; } = Array.Empty<Ciudad>();
    }
}
