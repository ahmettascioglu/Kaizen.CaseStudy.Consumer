using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Kaizen.CaseStudy.Consumer.Services.MailService
{
    public class MailService : IMailService
    {
        /// <summary>
        /// Sends OTP Code To Given Email Address
        /// </summary>
        /// <param name="code">OTP Code</param>
        /// <param name="emailTo">Email Address</param>
        public void SendMail(string code, string emailTo)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("from"));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = "Verification Code (One Time Password)";
            email.Body = new TextPart(TextFormat.Html) { Text = code };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("SmtpHost", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("SmtpUser", "SmtpPass");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        
        /// <summary>
        /// We Can Valide code and email address if they match or not. We can use Database system or Cache System In here
        /// </summary>
        /// <param name="code"></param>
        /// <param name="emailTo"></param>
        /// <returns></returns>
        public bool IsValid(string code, string emailTo)
        {
            return true;
        }
    }
}
