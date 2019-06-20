using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace InternetShopImplementations {
    public static class Mail {
        /// <summary>
        /// Отправление письма
        /// </summary>
        /// <param name="mailAddress">Может быть null, в таком случае получатель будет дефолтным</param>
        /// <param name="subject"></param>
        /// <param name="text"></param>
        /// <param name="fileName">Может быть null, в таком случае файл не будет прикреплен</param>
        public static void SendEmail(string mailAddress, string subject, string text, string fileName) {
            MailMessage objMailMessage = new MailMessage("trepixh@gmail.com",
            "pixert@mail.ru", subject, text);
            SmtpClient objSmtpClient;
            try {
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                if ( fileName != null ) {
                    objMailMessage.Attachments.Add(new Attachment(fileName));
                }
                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.Credentials = new NetworkCredential("trepixh@gmail.com",
                "jpdyykmibxgxdqfb");
                objSmtpClient.EnableSsl = true;
                objSmtpClient.Send(objMailMessage);
            }
            catch ( Exception ex ) {
                throw ex;
            }
            finally {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}