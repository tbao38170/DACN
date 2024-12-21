using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebApplication1.Service;

namespace WebApplication1.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient(_emailSettings.SMTPHost)
                {
                    Port = _emailSettings.SMTPPort,
                    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(toEmail);
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

    }
}
