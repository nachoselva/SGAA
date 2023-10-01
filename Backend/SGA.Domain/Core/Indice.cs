namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;
    using System.Collections.Generic;

    public class Indice : BaseEntity, IEntity
    {
        public Indice(IndiceTipo nombre)
        {
            Nombre = nombre;
        }

        public IndiceTipo Nombre { get; private set; }

        public IReadOnlyCollection<IndiceValor> Valores { get; private set; } = Array.Empty<IndiceValor>();
    }
}
