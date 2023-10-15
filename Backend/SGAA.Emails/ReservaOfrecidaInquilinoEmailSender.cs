namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class ReservaOfrecidaInquilinoEmailSender : BaseEmailSender<ReservaOfrecidaInquilinoEmailModel>, IReservaOfrecidaInquilinoEmailSender
    {
        public ReservaOfrecidaInquilinoEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Oferta recibida";
        public override string TemplateName => "ReservaOfrecidaInquilinoEmailTemplate";
    }
}
