namespace SGAA.Models
{
    using SGAA.Domain.Core;
    using SGAA.Models.Base;

    public class ContratoGetModel : IGetModel<Contrato>
    {
        public required int Id { get; set; }
        public required string Archivo { get; set; }
    }
}
