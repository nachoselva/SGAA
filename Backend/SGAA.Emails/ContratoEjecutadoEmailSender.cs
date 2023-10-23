namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class ContratoEjecutadoEmailSender : BaseEmailSender<ContratoEjecutadoEmailModel>, IContratoEjecutadoEmailSender
    {
        public ContratoEjecutadoEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Contrato ejecutado";
        public override string TemplateName => "ContratoEjecutadoEmailTemplate";
    }
}
