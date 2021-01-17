using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace StartAppModel.Manager
{
    public static class MailManager
    {
        private static StartAppEntities db = new StartAppEntities();

        /// <summary>
        /// Mail send function
        /// </summary>
        /// <param name="toMail"></param>
        /// <param name="title"></param>
        /// <param name="htmlBody"></param>
        public static void SendMail(string toMail, string title, string htmlBody)
        {
            MailConfigs config = db.MailConfigs.FirstOrDefault();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(config.FromMail);
            mailMessage.To.Add(new MailAddress(toMail));
            mailMessage.Subject = title;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = htmlBody;
            mailMessage.Bcc.Add(new MailAddress(config.BccMail));
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(config.SmtpEmail, config.SmtpPassword);
            client.Host = config.SmtpHost;

            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// special mail sending to user list
        /// </summary>
        /// <param name="toUsers"></param>
        /// <param name="title"></param>
        /// <param name="htmlBody"></param>
        public static void SendMail(List<string> toUsers, string title, string htmlBody)
        {
            MailConfigs config = db.MailConfigs.FirstOrDefault();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(config.FromMail);
            foreach (string email in toUsers)
            {
                mailMessage.Bcc.Add(new MailAddress(email));
            }
            mailMessage.Bcc.Add(new MailAddress(config.BccMail));
            mailMessage.Subject = title;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = htmlBody;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(config.SmtpEmail, config.SmtpPassword);
            client.Host = config.SmtpHost;

            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Mail will go to the new member
        /// </summary>
        /// <param name="user"></param>
        public static void SendNewUser(Users user)
        {
            string to = user.Email;
            string subject = GetSubject(PageSubject.NewUser);
            string html = DownloadHtml(GetValue("NewUserHtmlUrl"));
            //? html sayfalar ayarlandıktan sonra içerikleri string replace methodu ile burada değiştirilecek
            SendMail(to, subject, html);

        }

        /// <summary>
        /// We also want to send private mail to some users
        /// </summary>
        /// <param name="toUsers"></param>
        /// <param name="title"></param>
        /// <param name="html"></param>
        public static void SpecialMail(List<string> toUsers, string title, string html)
        {
            SendMail(toUsers, title, html);
        }


        //private
        /// <summary>
        /// Get appSettings data in app.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }

        /// <summary>
        /// Downloading the html file for mail html
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string DownloadHtml(string url)
        {
            string htmlCode = "";
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url);
            }
            return htmlCode;
        }

        /// <summary>
        /// This enum for page subject
        /// </summary>
        private enum PageSubject
        {
            NewUser = 1,
        }

        /// <summary>
        /// Get email subject
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private static string GetSubject(PageSubject page)
        {
            switch (page)
            {
                case PageSubject.NewUser:
                    return "Hoşgeldiniz";
                default:
                    return "";
            }
        }

    }
}
