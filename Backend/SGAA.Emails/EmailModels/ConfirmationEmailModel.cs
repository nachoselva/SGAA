namespace SGAA.Emails.EmailModels
{
    public class ConfirmationEmailModel : IEmailModel
    {
        public required string ConfirmationURL { get; set; }
    }
}
