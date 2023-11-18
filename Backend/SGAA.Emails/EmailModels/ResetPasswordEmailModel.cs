namespace SGAA.Emails.EmailModels
{
    public class ResetPasswordEmailModel : BaseEmailModel, IEmailModel
    {
        public required string ResetPasswordURL { get; set; }
    }
}
