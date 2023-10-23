namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class FirmaPendienteEmailSender : BaseEmailSender<FirmaPendienteEmailModel>, IFirmaPendienteEmailSender
    {
        public FirmaPendienteEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Firma de contrato pendiente";
        public override string TemplateName => "FirmaPendienteEmailTemplate";
    }
}
