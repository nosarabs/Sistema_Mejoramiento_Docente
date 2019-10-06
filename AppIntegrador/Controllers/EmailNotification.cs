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

namespace AppIntegrador.Controllers
{
    public class EmailNotification
    {
        private DataIntegradorEntities db;

        public EmailNotification()
        {
            db = new DataIntegradorEntities();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int SendNotification(List<string> receivers, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            try
            {
                var message = new MailMessage();
                // Add the receivers
                foreach (var receiver in receivers)
                {
                    message.Bcc.Add(new MailAddress(receiver)); // Bcc property used so that recipients dont's see other recipients
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

                // Mail settings in Web.config under system.net/mailSettings
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Send(message);
                    return 0;
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }
    }
}