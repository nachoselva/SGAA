namespace SGAA.Emails.EmailModels
{
    public class AprobarUnidadEmailModel : BaseEmailModel, IEmailModel
    {
        public required string Domicilio { get; set; }
    }
}
