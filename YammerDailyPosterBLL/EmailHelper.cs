using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YammerDailyPosterBLL
{
    public class EmailHelper
    {
        #region 引用前要修改的參數
        private string Myemail_service = System.Configuration.ConfigurationManager.AppSettings["mail_saas"];
        private string Myemail_Accound = System.Configuration.ConfigurationManager.AppSettings["mail_accound"];
        private string Myemail_Password = System.Configuration.ConfigurationManager.AppSettings["mail_password"];
        private MailAddress Default_MailSender = new MailAddress("robin@taifei.com.tw", "Robin");
        #endregion

        #region 共用屬性與方法

        public Exception ThrowException { get; set; }

        private System.Net.Mail.SmtpClient PrepareSmtp()
        {
            System.Net.Mail.SmtpClient smtp = new SmtpClient();

            if (Myemail_service== "Gmail")
                smtp.Host = "smtp.gmail.com";
            else
                smtp.Host = "smtp-mail.outlook.com";

            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(Myemail_Accound, Myemail_Password);
            return smtp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailToAddress">多人以 ";" 區隔</param>
        /// <param name="subjectText"></param>
        /// <param name="messageText"></param>
        /// <returns></returns>
        private MailMessage PrepareMail(string mailToAddress, string subjectText, string messageText)
        {
            MailMessage mail = new MailMessage();
            mail.From = Default_MailSender;

            foreach (string receiver in Regex.Split(mailToAddress, ";"))
                mail.To.Add(new MailAddress(receiver));

            mail.Subject = subjectText;
            mail.Body = messageText;
            mail.IsBodyHtml = false;
            return mail;
        }

        #endregion

        /// <summary>
        /// 傳送信件 - 同步傳送
        /// </summary>
        /// <param name="mailToAddress">收件者Email Address</param>
        /// <param name="subjectText">主旨</param>
        /// <param name="messageText">信件本文</param>
        /// <returns>tru: 傳送成功, false: 失敗, 請讀取ThrowException的錯誤訊息</returns>
        public bool Send(string mailToAddress, string subjectText, string messageText)
        {
            MailMessage mail = PrepareMail(mailToAddress, subjectText, messageText);
            SmtpClient smtp = PrepareSmtp();
            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    this.ThrowException = ex.InnerException;
                else
                    this.ThrowException = ex;

                return false;
            }
        }

        #region 非同步機制傳送Email

        public delegate void SendCompletedHandler(object sender, Exception e);
        public event SendCompletedHandler SendCompleted;
        /// <summary>
        /// 傳送信件 - 非同步傳送
        /// </summary>
        /// <param name="mailToAddress">收件者Email Address</param>
        /// <param name="subjectText">主旨</param>
        /// <param name="messageText">信件本文</param>
        public void SendSync(string mailToAddress, string subjectText, string messageText)
        {
            MailMessage mail = PrepareMail(mailToAddress, subjectText, messageText);
            SmtpClient smtp = PrepareSmtp();

            smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);

            smtp.SendAsync(mail, null);

        }

        void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (e.Error.InnerException != null)
                    this.ThrowException = e.Error.InnerException;
                else
                    this.ThrowException = e.Error;
            }
            else
            {
                this.ThrowException = null;
            }

            if (SendCompleted != null)
                SendCompleted(this, this.ThrowException);
        }

        #endregion
    }
}
