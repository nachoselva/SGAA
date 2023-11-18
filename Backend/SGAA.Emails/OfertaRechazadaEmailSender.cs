namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class OfertaRechazadaEmailSender : BaseEmailSender<OfertaRechazadaEmailModel>, IOfertaRechazadaEmailSender
    {
        public OfertaRechazadaEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Unidad reservada";
        public override string TemplateName => "OfertaRechazadaEmailTemplate";
    }
}
