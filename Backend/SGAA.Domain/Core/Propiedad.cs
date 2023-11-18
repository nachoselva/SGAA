namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System.Collections.Generic;

    public class Propiedad : BaseEntity, IEntity
    {
        public Propiedad(int ciudadId, string calle, int altura)
        {
            CiudadId = ciudadId;
            Calle = calle;
            Altura = altura;
        }

        public int CiudadId { get; private set; }
        public string Calle { get; private set; }
        public int Altura { get; private set; }

        public Ciudad Ciudad { get; private set; } = default!;
        public IReadOnlyCollection<Unidad> Unidades { get; private set; } = new List<Unidad>();
    }
}
