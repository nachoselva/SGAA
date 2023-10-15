namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class RechazarUnidadEmailSender : BaseEmailSender<RechazarUnidadEmailModel>, IRechazarUnidadEmailSender
    {
        public RechazarUnidadEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Documentación de unidad rechazada";
        public override string TemplateName => "RechazarUnidadEmailTemplate";
    }
}
