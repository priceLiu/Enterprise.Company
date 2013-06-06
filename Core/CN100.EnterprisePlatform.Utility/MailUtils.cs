// -----------------------------------------------------------------------
// <copyright file="MailUtils.cs" company="Microsoft">
// 邮件发送工具类
// </copyright>
// -----------------------------------------------------------------------

namespace CN100.EnterprisePlatform.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net.Mail;

    using CN100.EnterprisePlatform.Configuration;

    /// <summary>
    /// 邮件发送工具类
    /// </summary>
    public class MailUtils
    {
        const string CN100STRING = "中国第一百货";
        #region 单个邮件
        /// <summary>
        /// 发送单个邮件
        /// </summary>
        /// <param name="mailUrl">收件人邮箱</param>
        /// <param name="title">邮件标题</param>
        /// <param name="mailContent">邮件内容</param>
        /// <returns></returns>
        public  bool SendEmail(string mailUrl, string title, string mailContent)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(string.Format("{0}<{1}>", CN100STRING, Utils.EmailConfigHelper.Address),
                    CN100STRING, System.Text.Encoding.UTF8);//发件人信息
                msg.To.Add(mailUrl); //收件人的地址
                msg.Subject = title;   //邮件标题
                msg.SubjectEncoding = System.Text.Encoding.UTF8; //标题编码
                msg.Body = mailContent; //邮件内容
                msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
                msg.IsBodyHtml = true;  //是否HTML
                msg.Priority = MailPriority.Normal; //优先级
                SmtpClient client = new SmtpClient();
                client.Host = Utils.EmailConfigHelper.ServerName;//服务器物理地址
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(Utils.EmailConfigHelper.LoginAccount,
                    Utils.EmailConfigHelper.Password);//发件人的凭据
                client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件通过网络发送到SMTP服务器
                client.Send(msg);//发送邮件
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 单用户、单服务器发送邮件
        /// </summary>
        /// <param name="mailUrl">收件人地址</param>
        /// <param name="title">标题</param>
        /// <param name="mailFrom">发送地址 如：Server@CN100.com</param>
        /// <param name="mailContent">正文</param>
        /// <param name="mailHost">邮件服务器</param>
        /// <param name="mailUserName">用户名</param>
        /// <param name="mailUserPwd">密码</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        public  bool SendEmail(string mailUrl, string title, string mailContent, string mailFrom, string mailHost, string mailUserName, string mailUserPwd)
        {
            string companyName = "中国第一百货";
            try
            {
                MailMessage msg = new MailMessage();
                string fromAddress = string.Format(companyName + "{0}" + mailFrom + "{1}", "<", ">");
                msg.From = new MailAddress(fromAddress, companyName, System.Text.Encoding.UTF8);//发件人信息
                msg.To.Add(mailUrl); //收件人的地址
                msg.Subject = title;   //邮件标题
                msg.SubjectEncoding = System.Text.Encoding.UTF8; //标题编码
                msg.Body = mailContent; //邮件内容
                msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
                msg.IsBodyHtml = true;  //是否HTML
                msg.Priority = MailPriority.Normal; //优先级
                SmtpClient client = new SmtpClient();
                client.Host = mailHost;//服务器物理地址
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(mailUserName, mailUserPwd);//发件人的凭据
                client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件通过网络发送到SMTP服务器
                client.Send(msg);//发送邮件
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 邮件群发
        /// <summary>
        /// 邮件群发
        /// </summary>
        /// <param name="mailUrl">收件人邮箱</param>
        /// <param name="title">邮件标题</param>
        /// <param name="mailContent">邮件内容</param>
        /// <returns></returns>
        public  bool SendEmail(List<string> mailUrl, string title, string mailContent)
        {
            if (mailUrl == null || mailUrl.Count <= 0)
            {
                return false;
            }
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(string.Format("{0}<{1}>", CN100STRING, Utils.EmailConfigHelper.Address),
                   CN100STRING, System.Text.Encoding.UTF8);//发件人信息
                foreach (var item in mailUrl)
                {
                    msg.To.Add(item); //收件人的地址
                }

                MsgSend(title, mailContent, Utils.EmailConfigHelper.ServerName, Utils.EmailConfigHelper.LoginAccount,
                    Utils.EmailConfigHelper.Password, msg);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private  void MsgSend(string title, string mailContent, string mailHost, string mailUserName, string mailUserPwd, MailMessage msg)
        {
            msg.Subject = title;   //邮件标题
            msg.SubjectEncoding = System.Text.Encoding.UTF8; //标题编码
            msg.Body = mailContent; //邮件内容
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
            msg.IsBodyHtml = true;  //是否HTML
            msg.Priority = MailPriority.Normal; //优先级
            SmtpClient client = new SmtpClient();
            client.Host = mailHost;//服务器物理地址
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(mailUserName, mailUserPwd);//发件人的凭据
            client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件通过网络发送到SMTP服务器
            client.Send(msg);//发送邮件
        }



        public  bool SendEmails(List<string> mailUrl, string title, string mailContent)
        {
            if (mailUrl == null || mailUrl.Count <= 0)
            {
                return false;
            }
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(string.Format("{0}<{1}>", CN100STRING, Utils.EmailConfigHelper.Address),
                    CN100STRING, System.Text.Encoding.UTF8);//发件人信息
                foreach (var item in mailUrl)
                {
                    msg.To.Add(item); //收件人的地址
                }

                MsgSend(title, mailContent, Utils.EmailConfigHelper.ServerName, Utils.EmailConfigHelper.LoginAccount,
                   Utils.EmailConfigHelper.Password, msg);
                return true;
            }
            catch
            {
                return false;
            }
        }
       #endregion
    }
}
