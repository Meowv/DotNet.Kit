using System.Net;
using System.Net.Mail;
using System.Text;

namespace DotNet.Kit
{
    public class MailHelper
    {
        public static void SendMail(EmailEntity email)
        {
            //邮件服务设置
            var client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = email.MailHost,
                Credentials = new NetworkCredential(email.MailAccount, email.MailPassword)
            };

            //发送邮件设置
            var msg = new MailMessage(email.MailFrom, email.MailRecipient)
            {
                Subject = email.MailSubject,
                Body = email.MailBody,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Priority = MailPriority.Normal
            };

            //发送邮件
            client.Send(msg);
        }
    }

    public class EmailEntity
    {
        /// <summary>
        /// 收件人地址
        /// </summary>
        public string MailRecipient { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string MailSubject { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string MailBody { get; set; }
        /// <summary>
        /// 发件人地址
        /// </summary>
        public string MailFrom { get; set; }
        /// <summary>
        /// SMTP服务器
        /// </summary>
        public string MailHost { get; set; }
        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string MailAccount { get; set; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string MailPassword { get; set; }
    }
}