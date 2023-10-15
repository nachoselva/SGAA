namespace SGAA.Emails.EmailModels
{
    public class AprobarAplicacionEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
    }
}
