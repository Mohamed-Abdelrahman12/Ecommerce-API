
using Ecom.Application.DTOs;
using Ecom.Core.Services;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Ecom.infrastructure.Repositories.service
{
    public class EmailService : IEmailService
    {
        //SMTP

        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        async Task IEmailService.SendEmail(EmailDTO emailDTO)
        {
            MimeMessage message = new();

            message.From.Add(  new MailboxAddress("Shopping Ecommerce" ,_configuration["EmailSetting:From"]));
            message.Subject = emailDTO.Subject;
            message.To.Add(new MailboxAddress( emailDTO.SendTo,emailDTO.SendTo));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDTO.Content
            };

            using(var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(_configuration["EmailSetting:Smtp"]
                        , int.Parse(_configuration["EmailSetting:Port"]), SecureSocketOptions.StartTls);

                    await smtp.AuthenticateAsync(_configuration["EmailSetting:Username"], 
                        _configuration["EmailSetting:Password"]);

                    await smtp.SendAsync(message);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }
        }
    }
}
