using EmailSender.Protos;

namespace Traders.gRPCservice
{
    public class MailgRPCservice
    {
        private readonly Mailer.MailerClient _mailer;
        public MailgRPCservice(Mailer.MailerClient mailer)
        {
            _mailer = mailer;
        }

        public async Task SendEmail(string email , string code)
        {
            var request = new GetUserNameRequest { Email = email, Code = code };
            await _mailer.SendEmailAsync(request);
        }
    }
}
