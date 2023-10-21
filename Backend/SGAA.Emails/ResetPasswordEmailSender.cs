namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class ResetPasswordEmailSender : BaseEmailSender<ResetPasswordEmailModel>, IResetPasswordEmailSender
    {
        public ResetPasswordEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Oferta enviada";
        public override string TemplateName => "ResetPasswordEmailTemplate";
    }
}
