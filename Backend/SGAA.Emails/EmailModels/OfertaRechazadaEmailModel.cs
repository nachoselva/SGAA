namespace SGAA.Emails.EmailModels
{
    public class OfertaRechazadaEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
    }
}
