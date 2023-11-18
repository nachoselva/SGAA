namespace SGAA.Emails.EmailModels
{
    public class FirmaPendienteEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
    }

}
