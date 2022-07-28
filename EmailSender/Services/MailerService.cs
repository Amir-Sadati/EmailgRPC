using EmailSender.MailService;
using EmailSender.Protos;
using Grpc.Core;

namespace EmailSender.Services
{
    public class MailerService :Mailer.MailerBase
    {
        private readonly IEmailService _emailService;

        public MailerService(IEmailService emailService)
        {
            _emailService = emailService;
        }


        public override async Task<EmailStatus> SendEmail(GetUserNameRequest request, ServerCallContext context)
        {

            await _emailService.SendEmailAsync(request.Email, "Email Verification", $"می باشد{request.Code} کد فعالسازی شما");
            return new EmailStatus { Success = true };

        }

    }
}
