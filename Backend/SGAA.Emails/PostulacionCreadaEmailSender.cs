namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class PostulacionCreadaEmailSender : BaseEmailSender<PostulacionCreadaEmailModel>, IPostulacionCreadaEmailSender
    {
        public PostulacionCreadaEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Postulación recibida";
        public override string TemplateName => "PostulacionCreadaEmailTemplate";
    }
}
