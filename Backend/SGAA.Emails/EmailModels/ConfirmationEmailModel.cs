namespace SGAA.Emails.EmailModels
{
    public class ConfirmationEmailModel : BaseEmailModel, IEmailModel
    {
        public required string ConfirmationURL { get; set; }
    }
}
