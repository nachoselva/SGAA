namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class PublicarUnidadEmailSender : BaseEmailSender<PublicarUnidadEmailModel>, IPublicarUnidadEmailSender
    {
        public PublicarUnidadEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Unidad publicada";
        public override string TemplateName => "PublicarUnidadEmailEmailTemplate";
    }
}
