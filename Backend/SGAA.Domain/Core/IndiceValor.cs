namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class IndiceValor : BaseEntity, IEntity
    {
        public IndiceValor(int indiceId, DateTime fechaDesde, decimal valor)
        {
            IndiceId = indiceId;
            FechaDesde = fechaDesde;
            Valor = valor;
        }

        public int IndiceId { get; private set; }
        public DateTime FechaDesde { get; private set; }
        public decimal Valor { get; private set; }
        public Indice Indice { get; } = default!;
    }
}
