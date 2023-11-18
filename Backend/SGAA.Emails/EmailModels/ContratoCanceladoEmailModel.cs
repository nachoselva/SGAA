namespace SGAA.Emails.EmailModels
{
    public class ContratoCanceladoEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
        public required string FechaCancelacion { get; set; }
    }

}
