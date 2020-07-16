namespace Shop.Services
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    public class NullMailService : IMailService
    {
        private readonly ILogger<NullMailService> logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            this.logger = logger;
        }

        public Task SendEmailAsync(string toEmail, string subject, string content)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string to, string subject, string body)
        {
            // Log the message
            this.logger.LogInformation($"To: {to} subject: {subject} body: {body}.");
        }
    }
}
