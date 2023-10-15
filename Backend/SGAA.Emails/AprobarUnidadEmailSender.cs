namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class AprobarUnidadEmailSender : BaseEmailSender<AprobarUnidadEmailModel>, IAprobarUnidadEmailSender
    {
        public AprobarUnidadEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Documentación de unidad aprobada";
        public override string TemplateName => "AprobarUnidadEmailTemplate";
    }
}
