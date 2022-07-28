using EmailSender.Config;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace EmailSender.MailService
{
    public class EmailService : IEmailService
    {
        
        private readonly EmailConfig _emailConfig;

        public EmailService(IOptions<EmailConfig> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendEmailAsync(string to,string subject,string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("traders.com",_emailConfig.From));
            emailMessage.To.Add(new MailboxAddress("amir",to));
            emailMessage.Subject =subject;
            emailMessage.Body = new TextPart(TextFormat.Html);
            await SendAsync(emailMessage);
        }
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                   await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                   await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                   await client.SendAsync(mailMessage);
                }
                catch
                {
                   // we can use logger
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
