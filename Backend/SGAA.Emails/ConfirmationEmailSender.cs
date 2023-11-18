namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
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
