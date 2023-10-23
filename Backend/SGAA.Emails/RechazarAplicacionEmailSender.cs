namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class RechazarAplicacionEmailSender : BaseEmailSender<RechazarAplicacionEmailModel>, IRechazarAplicacionEmailSender
    {
        public RechazarAplicacionEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Documentación de aplicación rechazada";
        public override string TemplateName => "RechazarAplicacionEmailTemplate";
    }
}
