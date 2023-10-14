namespace SGAA.Emails.Base
{
    using HandlebarsDotNet;
    using SGAA.Emails.EmailModels;
    using SGAA.Utils.Configuration;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;

    public abstract class BaseEmailSender<T>
        where T : IEmailModel
    {
        private static readonly string path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!}\\EmailTemplates\\";
        private readonly ISGAAConfiguration _configuration;
        private readonly IHandlebars _handlebarEngine = Handlebars.Create();

        public BaseEmailSender(ISGAAConfiguration configuration)
        {
            _handlebarEngine = Handlebars.Create();
            _configuration = configuration;
        }

        public abstract string Subject { get; }

        public abstract string TemplateName { get; }

        private string IntegrateLayout(string partialBody)
        {
            string templateContent = File.ReadAllText($"{path}EmailLayout.html");
            var template = _handlebarEngine.Compile(templateContent);
            var data = new { partialBody };
            string fullBody = template(data);
            return fullBody;
        }

        private string GetEmailBody(T data)
        {
            string templateContent = File.ReadAllText($"{path}{TemplateName}.html");
            var template = _handlebarEngine.Compile(templateContent);
            string body = template(data);
            return IntegrateLayout(body);
        }

        private async Task SendEmail(string toAddress, string subject, string body)
        {
            string smtpHost = _configuration.Smtp.Host;
            int smtpPort = _configuration.Smtp.Port;
            string smtpUsername = _configuration.Smtp.Username;
            string smtpPassword = _configuration.Smtp.Password;
            bool enableSsl = _configuration.Smtp.EnableSsl;

            using SmtpClient smtpClient = new(smtpHost, smtpPort);

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = enableSsl;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toAddress);

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendEmail(string toAddress, T data)
        {
            string body = GetEmailBody(data);
            string subject = "SGAA - " + Subject;
            await SendEmail(toAddress, subject, body);
        }
    }
}
