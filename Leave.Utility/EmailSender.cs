using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Leave.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromAddress = _config["EmailSettings:DefaultEmailAddress"];
            var smtpServer = _config["EmailSettings:Server"];
            var smtpPort =Convert.ToInt32(_config["EmailSettings:Port"]);
            var message = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(email));
            using var client =new SmtpClient(smtpServer, smtpPort);
            await client.SendMailAsync(message);
        }
    }
}
