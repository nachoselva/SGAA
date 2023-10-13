namespace SGAA.Emails
{
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class ConfirmationEmailSender : BaseEmailSender<ConfirmationEmailModel>, IConfirmationEmailSender
    {
        public ConfirmationEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Confirmación de correo electrónico";
        public override string TemplateName => "ConfirmationEmailTemplate";
    }
}
