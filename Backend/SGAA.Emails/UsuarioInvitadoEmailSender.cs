namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class UsuarioInvitadoEmailSender : BaseEmailSender<UsuarioInvitadoEmailModel>, IUsuarioInvitadoEmailSender
    {
        public UsuarioInvitadoEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Invitación";
        public override string TemplateName => "UsuarioInvitadoEmailTemplate";
    }
}
