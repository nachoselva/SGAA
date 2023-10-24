namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System.Collections.Generic;

    public class Indice : BaseEntity, IEntity
    {
        public Indice(int id, IndiceTipo nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public IndiceTipo Nombre { get; private set; }

        public IReadOnlyCollection<IndiceValor> Valores { get; private set; } = new List<IndiceValor>();
    }
}
