using System.Net.Mail;

namespace Samples.MailService
{
    class MailService
    {
        private ILogger logger;

        public MailService(ILogger logger)
        {
            this.logger = logger;
        }

        public void SendMail(string address, string subject, string body)
        {
            logger.Log("Creating mail message...");
            var mail = new MailMessage();
            mail.To.Add(address);
            mail.Subject = subject;
            mail.Body = body;
            var client = new SmtpClient();
            // Setup client with smtp server address and port here
            logger.Log("Sending message...");
            client.Send(mail);
            logger.Log("Message sent successfully.");
        }
    }
}