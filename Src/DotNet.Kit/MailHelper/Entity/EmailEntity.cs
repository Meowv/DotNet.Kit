namespace DotNet.Kit.MailHelper.Entity
{
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