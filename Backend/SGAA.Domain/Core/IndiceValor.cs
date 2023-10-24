namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class IndiceValor : BaseEntity, IEntity
    {
        public IndiceValor(int indiceId, int id, DateOnly fechaDesde, decimal valor)
        {
            IndiceId = indiceId;
            Id = id;
            FechaDesde = fechaDesde;
            Valor = valor;
        }

        public int IndiceId { get; private set; }
        public DateOnly FechaDesde { get; private set; }
        public decimal Valor { get; private set; }
        public Indice Indice { get; } = default!;
    }
}
