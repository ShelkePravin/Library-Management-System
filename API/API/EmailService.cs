using System.Net;
using System.Net.Mail;

namespace API
{
    public class EmailService
    {
        public EmailService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void SendEmail(string toEmail, string subject, string body)
        {
            var fromEmail = Configuration["Constants:FromEmail"] ?? string.Empty;
            var fromEmailPassword = Configuration["Constants:EmailAccountPassword"] ?? string.Empty;

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(fromEmail);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.To.Add(toEmail);

                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);
                }
            }
        }
    }
}
