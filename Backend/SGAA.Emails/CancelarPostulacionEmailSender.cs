﻿namespace SGAA.Emails
{
    using SGAA.Emails.Base;
    using SGAA.Emails.Contracts;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;

    public class CancelarPostulacionEmailSender : BaseEmailSender<CancelarPostulacionEmailModel>, ICancelarPostulacionEmailSender
    {
        public CancelarPostulacionEmailSender(ISGAAConfiguration configuration) : base(configuration)
        {
        }

        public override string Subject => "Postulación cancelada";
        public override string TemplateName => "CancelarPostulacionEmailTemplate";
    }
}
