using System.Net;
using System.Net.Mail;

namespace CoreDevextremeTheme.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Task.CompletedTask;
            }

            //check if email is valid
            if (!new EmailAddressAttribute().IsValid(email))
            {
                return Task.CompletedTask;
            }

            #region TestCode
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "mail.sargotur.com", //or another email sender provider
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("info@sargotur.com", "6ttTZ5gcaTdWwkW")
            };

            return client.SendMailAsync("info@sargotur.com", email, subject, htmlMessage);
            #endregion

            //SmtpClient client = new SmtpClient
            //{
            //    Port = 587,
            //    Host = "10.1.0.1",                
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential("abalioglu/ft.ulak", "for9eF!l")
            //};

            //return client.SendMailAsync("ulak@filidea.com.tr", email, subject, htmlMessage);            
        }
    }
}
