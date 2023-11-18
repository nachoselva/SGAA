namespace SGAA.Emails.EmailModels
{
    public class PostulacionCreadaEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
    }
}
