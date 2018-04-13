using DotNet.Kit.MailHelper.Entity;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DotNet.Kit.MailHelper
{
    public class MailHelper
    {
        public void SendMail(EmailEntity email)
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
}