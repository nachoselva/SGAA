namespace SGAA.Emails.Contracts
{
    using SGAA.Emails.EmailModels;

    public interface IEmailSender<T>
        where T : IEmailModel
    {
        public string Subject { get; }

        public string TemplateName { get; }

        public Task SendEmail(string toAddress, T data);
    }
}
