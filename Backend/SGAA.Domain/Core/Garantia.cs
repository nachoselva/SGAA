namespace SGAA.Domain.Core
{
    using SGAA.Domain.Base;

    public class Garantia : BaseEntity, IEntity
    {
        public Garantia(int aplicacionId, decimal monto, string archivo)
        {
            AplicacionId = aplicacionId;
            Monto = monto;
            Archivo = archivo;
        }

        public int AplicacionId { get; set; }
        public decimal Monto { get; set; }
        public string Archivo { get; set; }

        public Aplicacion Aplicacion { get; private set; } = default!;
    }
}
