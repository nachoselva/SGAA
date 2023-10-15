namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class AprobarAplicacionEmailSender : BaseEmailSender<AprobarAplicacionEmailModel>, IAprobarAplicacionEmailSender
    {
        public AprobarAplicacionEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Documentación de aplicación aprobada";
        public override string TemplateName => "AprobarAplicacionEmailTemplate";
    }
}
