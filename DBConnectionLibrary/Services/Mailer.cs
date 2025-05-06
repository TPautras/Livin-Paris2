using System;
using System.Net;
using System.Net.Mail;

namespace SqlConnector.Services
{
    public static class Mailer
    {
        public static string SendPasswordReminder(string toEmail, string password)
        {
            var fromAddress = new MailAddress("livinparis0@gmail.com", "Livin'Paris");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "vbgc lubf hvyw kgcj";
            const string subject = "Récupération de mot de passe - Livin'Paris";
            Random random = new Random();
            string toRec = random.Next(100000, 999999).ToString();
            string body = $"Votre code de recuperation est : {toRec}";

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
            return toRec;
        }
    }
}