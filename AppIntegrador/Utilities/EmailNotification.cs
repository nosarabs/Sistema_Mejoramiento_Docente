using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Utilities
{
    /* Historia TAM-5.2 Interfaz de envío de correos */
    public class EmailNotification
    {
        private DataIntegradorEntities db;

        public EmailNotification()
        {
            db = new DataIntegradorEntities();
        }

        /// <summary>Este método envía una notificación de correo directa
        /// utilizando los datos provistos y la configuración de correo en Web.config.</summary>
        /// <param name="recipients">Lista de receptores.</param>
        /// <param name="subject">El asunto del correo.</param>
        /// <param name="bodyPlainText">El cuerpo del correo en texto plano.</param>
        /// <param name="bodyAlternateHtml">El cuerpo del correo en HTML para ser enviado como alternativa.</param>
        /// <returns>0 si tiene éxito, -1 en otro caso</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int SendNotification(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            try
            {
                using (MailMessage message = ConstructMessage(recipients, subject, bodyPlainText, bodyAlternateHtml, 'd'))
                {
                    // Mail settings in Web.config under system.net/mailSettings
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                        return 0;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        /// <summary>Este método envía una notificación de correo directa (de forma asíncrona)
        /// utilizando los datos provistos y la configuración de correo en Web.config.</summary>
        /// <param name="recipients">Lista de receptores.</param>
        /// <param name="subject">El asunto del correo.</param>
        /// <param name="bodyPlainText">El cuerpo del correo en texto plano.</param>
        /// <param name="bodyAlternateHtml">El cuerpo del correo en HTML para ser enviado como alternativa.</param>
        /// <returns>0 si tiene éxito, -1 en otro caso</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<int> SendNotificationAsync(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            try
            {
                using (MailMessage message = ConstructMessage(recipients, subject, bodyPlainText, bodyAlternateHtml, 'd'))
                {
                    // Mail settings in Web.config under system.net/mailSettings
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(message).ConfigureAwait(false);
                        return 0;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        /// <summary>Este método envía una notificación de correo con copia (CC)
        /// utilizando los datos provistos y la configuración de correo en Web.config.</summary>
        /// <param name="recipients">Lista de receptores.</param>
        /// <param name="subject">El asunto del correo.</param>
        /// <param name="bodyPlainText">El cuerpo del correo en texto plano.</param>
        /// <param name="bodyAlternateHtml">El cuerpo del correo en HTML para ser enviado como alternativa.</param>
        /// <returns>0 si tiene éxito, -1 en otro caso</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int SendNotificationCopy(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            try
            {
                using (MailMessage message = ConstructMessage(recipients, subject, bodyPlainText, bodyAlternateHtml, 'c'))
                {
                    // Mail settings in Web.config under system.net/mailSettings
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                        return 0;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        /// <summary>Este método envía una notificación de correo con copia (CC) (de forma asíncrona)
        /// utilizando los datos provistos y la configuración de correo en Web.config.</summary>
        /// <param name="recipients">Lista de receptores.</param>
        /// <param name="subject">El asunto del correo.</param>
        /// <param name="bodyPlainText">El cuerpo del correo en texto plano.</param>
        /// <param name="bodyAlternateHtml">El cuerpo del correo en HTML para ser enviado como alternativa.</param>
        /// <returns>0 si tiene éxito, -1 en otro caso</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<int> SendNotificationCopyAsync(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            try
            {
                using (MailMessage message = ConstructMessage(recipients, subject, bodyPlainText, bodyAlternateHtml, 'c'))
                {
                    // Mail settings in Web.config under system.net/mailSettings
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(message).ConfigureAwait(false);
                        return 0;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        /// <summary>Este método envía una notificación de correo con copia oculta (CCO)
        /// utilizando los datos provistos y la configuración de correo en Web.config.</summary>
        /// <param name="recipients">Lista de receptores.</param>
        /// <param name="subject">El asunto del correo.</param>
        /// <param name="bodyPlainText">El cuerpo del correo en texto plano.</param>
        /// <param name="bodyAlternateHtml">El cuerpo del correo en HTML para ser enviado como alternativa.</param>
        /// <returns>0 si tiene éxito, -1 en otro caso</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int SendNotificationBlindCopy(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            try
            {
                using (MailMessage message = ConstructMessage(recipients, subject, bodyPlainText, bodyAlternateHtml, 'b'))
                {
                    // Mail settings in Web.config under system.net/mailSettings
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                        return 0;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        /// <summary>Este método envía una notificación de correo con copia oculta (CCO) (de forma asíncrona)
        /// utilizando los datos provistos y la configuración de correo en Web.config.</summary>
        /// <param name="recipients">Lista de receptores.</param>
        /// <param name="subject">El asunto del correo.</param>
        /// <param name="bodyPlainText">El cuerpo del correo en texto plano.</param>
        /// <param name="bodyAlternateHtml">El cuerpo del correo en HTML para ser enviado como alternativa.</param>
        /// <returns>0 si tiene éxito, -1 en otro caso</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<int> SendNotificationBlindCopyAsync(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml)
        {
            try
            {
                using (MailMessage message = ConstructMessage(recipients, subject, bodyPlainText, bodyAlternateHtml, 'b'))
                {
                    // Mail settings in Web.config under system.net/mailSettings
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(message).ConfigureAwait(false);
                        return 0;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return -1;
        }

        private MailMessage ConstructMessage(List<string> recipients, string subject, string bodyPlainText, string bodyAlternateHtml, char msgType)
        {
            MailMessage message = new MailMessage();
            // Agregar la lista de receptores, según el tipo de mensaje
            foreach (string recipient in recipients)
            {
                switch (msgType)
                {
                    // Mensaje directo
                    case 'd':
                        message.To.Add(new MailAddress(recipient));
                        break;

                    // Con copia
                    case 'c':
                        message.CC.Add(new MailAddress(recipient));
                        break;

                    // Con copia oculta
                    case 'b':
                        message.Bcc.Add(new MailAddress(recipient));
                        break;
                }                
            }
            message.Subject = subject;
            // Body just in plain text
            message.Body = bodyPlainText;
            
            // Construct the alternate body as HTML.
            // Construir el Header como los correos institucionales
            string htmlHeader = "< div id = \"encabezado\" style = \"width:100%;	display:block; background:#47ABE1; height:100px;\" >" +
                                    "< div style = \"max-width:975px;margin-left:auto;margin-right:auto; height:70px; padding-top:10px;\" >" +
                                        "< a href = \"http://www.ucr.ac.cr\" id = \"firma\" style = \"border:none;\" >< img src = \"../Content/images/logo_ucr.png\" " +
                                        "border = \"0\" id = \"encabezado-imagen\" alt = \"Universidad de Costa Rica\" /></ a ></ div >" +
                                    "< div style = \"background:#00c0f3;; height:20px;\" > &nbsp;</ div >" +
                                "</ div >";

            string htmlBody = bodyAlternateHtml;

            // Construir el Footer como los correos institucionales
            string htmlFooter = "< div style = \"margin:0;padding:0 20px 0 20px;background:#005da4;color:#fff;min-height:120px\" >" +
                                    "< div style = \"max-width:975px;min-width:320px;margin-left:auto;margin-right:auto;height:70px;padding-top:40px\" >" +
                                        "< p style = \"margin:0px;font-family:Verdana,Arial;font-size:11px;color:#fff\" > < b > Tel.</ b > 2511 - 1213 </ p >" +
                                        "< p style = \"font-size:9px;font-family:Verdana,Arial;font-weight:bold;color:#fff\" >© 2019 Universidad de " +
                                            "Costa Rica</ p >" +
                                    "</ div >" +
                                "</ div >";

            ContentType mimeType = new ContentType("text/html");
            // Add the alternate body to the message.

            //AlternateView alternate = AlternateView.CreateAlternateViewFromString(htmlHeader + htmlBody + htmlFooter, mimeType);
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(htmlBody, mimeType);

            message.AlternateViews.Add(alternate);
            return message;
        }
    }
}