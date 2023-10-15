namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class ReservaOfrecidaPropietarioEmailSender : BaseEmailSender<ReservaOfrecidaPropietarioEmailModel>, IReservaOfrecidaPropietarioEmailSender
    {
        public ReservaOfrecidaPropietarioEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Oferta enviada";
        public override string TemplateName => "ReservaOfrecidaPropietarioEmailTemplate";
    }
}
