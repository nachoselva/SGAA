namespace SGAA.Emails.EmailModels
{
    public class OfertaAceptadaEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
    }
}
