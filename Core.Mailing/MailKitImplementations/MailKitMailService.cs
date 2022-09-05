using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mailing.MailKitImplementations
{
    public class MailKitMailService : IMailService
    {
        private IConfiguration _configuration;
        private readonly MailSettings _mailSettings;

        public MailKitMailService(IConfiguration configuration, MailSettings mailSettings)
        {
            _configuration = configuration;
            _mailSettings = mailSettings;
        }

        public void SendMail(Mail mail)
        {
            MimeMessage email = new();


            email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderEmail));

            email.To.Add(new MailboxAddress(mail.ToFullName, mail.ToEmail));

            email.Subject = mail.Subject;

            BodyBuilder bodyBuilder = new()
            {
                TextBody = mail.TextBody,
                HtmlBody = mail.HtmlBody
            };

            if (mail.Attachments != null)
                foreach (MimeEntity? attachment in mail.Attachments)
                    bodyBuilder.Attachments.Add(attachment);

            email.Body = bodyBuilder.ToMessageBody();

            using SmtpClient smtp = new();
            smtp.Connect(_mailSettings.Server, _mailSettings.Port);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
