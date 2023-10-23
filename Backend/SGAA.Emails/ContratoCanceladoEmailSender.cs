namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class ContratoCanceladoEmailSender : BaseEmailSender<ContratoCanceladoEmailModel>, IContratoCanceladoEmailSender
    {
        public ContratoCanceladoEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Contrato Cancelado";
        public override string TemplateName => "ContratoCanceladoEmailTemplate";
    }
}
