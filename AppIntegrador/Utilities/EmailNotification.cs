using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Utilities
{
    public class EmailNotification
    {
        private DataIntegradorEntities db;

        public EmailNotification()
        {
            db = new DataIntegradorEntities();
        }


        /// <summary>This method sends an email notification using the provided info
        /// and Web.config mail settings.</summary>
        /// <param name="recipients">List of recipients.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="bodyPlainText">The email body in plain text.</param>
        /// <param name="bodyAlternateHtml">The email body in HTML to be sent as an alternate body.</param>
        /// <returns>0 on success, -1 otherwise</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int SendNotification(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            try
            {
                MailMessage message = ConstructMessage(recipients, subject, bodyPlainText, bodyAlternateHtml);

                // Mail settings in Web.config under system.net/mailSettings
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Send(message);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        /// <summary>This method sends an email notification (asynchronously)
        /// using the provided info and Web.config mail settings.</summary>
        /// <param name="recipients">List of recipients.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="bodyPlainText">The email body in plain text.</param>
        /// <param name="bodyAlternateHtml">The email body in HTML to be sent as an alternate body.</param>
        /// <returns>0 on success, -1 otherwise</returns>
        public async Task<int> SendNotificationAsync(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            try
            {
                MailMessage message = ConstructMessage(recipients, subject, bodyPlainText, bodyAlternateHtml);

                // Mail settings in Web.config under system.net/mailSettings
                using (SmtpClient smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        private MailMessage ConstructMessage(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            MailMessage message = new MailMessage();
            // Add the recipients
            foreach (string recipient in recipients)
            {
                message.Bcc.Add(new MailAddress(recipient)); // Bcc property used so that recipients dont's see other recipients
            }
            message.Subject = subject;
            // Body just in plain text
            message.Body = bodyPlainText;
            // Construct the alternate body as HTML.
            string htmlBody = bodyAlternateHtml;

            ContentType mimeType = new ContentType("text/html");
            // Add the alternate body to the message.

            AlternateView alternate = AlternateView.CreateAlternateViewFromString(htmlBody, mimeType);
            message.AlternateViews.Add(alternate);
            return message;
        }
    }
}