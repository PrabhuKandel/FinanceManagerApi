    using MailKit.Net.Smtp;
using FinanceManager.Application.Interfaces.Services;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;


namespace FinanceManager.Infrastructure.Services
{
    public class MailKitEmailService(IConfiguration _configuration) : IEmailService
    {
        public async  Task SendEmailAsync(string to, string subject ,string body)
        {
            // Read SMTP settings directly from configuration
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var smtpUser = _configuration["EmailSettings:SmtpUser"];
            var smtpPass = _configuration["EmailSettings:SmtpPass"];
            var senderName = _configuration["EmailSettings:SenderName"];
            var senderEmail = _configuration["EmailSettings:SenderEmail"];



            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(senderName, senderEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(smtpUser, smtpPass);
            await client.SendAsync(email);
            await client.DisconnectAsync(true);



        }
    }
}
