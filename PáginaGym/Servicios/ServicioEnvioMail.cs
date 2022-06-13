using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PáginaGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Threading.Tasks;

namespace PáginaGym.Servicios
{    public class ServicioEnvioMail : IEnvioMail
    {
        private readonly ConfiguracionMail _configuracionMail;
        public ServicioEnvioMail(IOptions<ConfiguracionMail> configuracionmail)
        {
            _configuracionMail = configuracionmail.Value;

        }

        public async Task SendEmailAsync(EnvioMail mailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(mailRequest.From, mailRequest.Name));
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_configuracionMail.Host, _configuracionMail.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuracionMail.Mail, _configuracionMail.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

    }
}
