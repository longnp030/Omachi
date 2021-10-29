using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Services
{
    public class EmailService
    {
        #region Properties
        private readonly MimeMessage _message;
        private readonly MailboxAddress _from;
        private readonly MailboxAddress _to;

        [Obsolete]
        private readonly IHostingEnvironment _env;
        #endregion Properties

        #region Constructors
        public EmailService(String toEmail)
        {
            _message = new MimeMessage();
            _from = new MailboxAddress("Admin", "akisukidesuyo@gmail.com");
            _to = new MailboxAddress("User", toEmail);
            _message.From.Add(_from);
            _message.To.Add(_to);
        }

        public EmailService(String toEmail, String subject)
        {
            _message = new MimeMessage();
            _from = new MailboxAddress("Admin", "akisukidesuyo@gmail.com");
            _to = new MailboxAddress("User", toEmail);
            _message.From.Add(_from);
            _message.To.Add(_to);
            _message.Subject = subject;
        }

        public EmailService(String toEmail, String subject, String body)
        {
            _message = new MimeMessage();
            _from = new MailboxAddress("Admin", "akisukidesuyo@gmail.com");
            _to = new MailboxAddress("User", toEmail);
            _message.From.Add(_from);
            _message.To.Add(_to);
            _message.Subject = subject;

            BodyBuilder bodyBuilder = new();
            bodyBuilder.HtmlBody = $"<h1>{body}</h1>";
            bodyBuilder.TextBody = body;
            _message.Body = bodyBuilder.ToMessageBody();
        }

        [Obsolete]
        public EmailService(IHostingEnvironment env, String toEmail, String subject, String body, String attachFilePath)
        {
            _env = env;

            _message = new MimeMessage();
            _from = new MailboxAddress("Admin", "akisukidesuyo@gmail.com");
            _to = new MailboxAddress("User", toEmail);
            _message.From.Add(_from);
            _message.To.Add(_to);
            _message.Subject = subject;

            BodyBuilder bodyBuilder = new();
            bodyBuilder.HtmlBody = $"<h1>{body}</h1>";
            bodyBuilder.TextBody = body;
            bodyBuilder.Attachments.Add(_env.WebRootPath + $"\\{attachFilePath}");
            _message.Body = bodyBuilder.ToMessageBody();
        }
        #endregion Constructors

        #region Methods
        public void AddSubject(String subject)
        {
            _message.Subject = subject;
        }

        public void AddBody(String body)
        {
            BodyBuilder bodyBuilder = new();
            bodyBuilder.HtmlBody = $"<h1>{body}</h1>";
            bodyBuilder.TextBody = body;
            _message.Body = bodyBuilder.ToMessageBody();
        }

        [Obsolete]
        public void AddBodyWithAttachment(String body, String attachFilePath)
        {
            BodyBuilder bodyBuilder = new();
            bodyBuilder.HtmlBody = $"<h1>{body}</h1>";
            bodyBuilder.TextBody = body;
            bodyBuilder.Attachments.Add(_env.WebRootPath + $"\\{attachFilePath}");
            _message.Body = bodyBuilder.ToMessageBody();
        }

        public void Send()
        {
            var _client = new SmtpClient();
            _client.Connect("smtp.gmail.com", 465, true);
            _client.Authenticate("akisukidesuyo@gmail.com", "philongg2000@gmail.com");
            _client.Send(_message);
            _client.Disconnect(true);
            _client.Dispose();
        }
        #endregion Methods
    }
}
