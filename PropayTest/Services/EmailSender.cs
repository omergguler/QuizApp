
using System.Net;
using System.Net.Mail;

namespace PropayTest.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "omerguler2000@hotmail.com";
            var pw = "JK89yu64#m";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                to: email, subject, message));
        }
    }
}
