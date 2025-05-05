using System.Net;
using System.Net.Mail;

namespace SqlConnector.Services
{
    public static class Mailer
    {
        public static void SendPasswordReminder(string toEmail, string password)
        {
            var fromAddress = new MailAddress("livinparis0@gmail.com", "Livin'Paris");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "vbgc lubf hvyw kgcj";
            const string subject = "Récupération de mot de passe - Livin'Paris";
            string body = $"Votre mot de passe est : {password}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
                   {
                       Subject = subject,
                       Body = body
                   })
            {
                smtp.Send(message);
            }
        }
    }
}