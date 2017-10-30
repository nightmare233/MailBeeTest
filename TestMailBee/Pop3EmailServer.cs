using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailBee.Pop3Mail;
using MailBee;
using MailBee.Mime;
using log4net;

namespace TestMailBee
{
    public class Pop3EmailServer
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger("test.log");//获取一个日志记录器
        public static void GetQuickDownloadLastMail()
        {
            try
            {
                //Pop3 pop3 = new Pop3();
                log.Info("Begin to receive email");
                MailMessage mailMessage = MailBee.Pop3Mail.Pop3.QuickDownloadMessage("pop3.126.com", "frankfeng23@126.com", "Aa00000000", -1);
                log.Info("End to receive email.");
                log.Info("subject: " + mailMessage.Subject);
                log.Info("if has attachment: " + mailMessage.HasAttachments.ToString());
                log.Info("mail size: " + mailMessage.Size);
                //log.Info("HTML Body: " + mailMessage.BodyHtmlText);
                //log.Info("Pain Body: " + mailMessage.BodyPlainText);
                string body = string.IsNullOrEmpty(mailMessage.BodyHtmlText) ? mailMessage.BodyPlainText : mailMessage.BodyHtmlText;
                log.Info("Body: " + body);
                if (mailMessage.HasAttachments)
                {
                    foreach (Attachment attachment in mailMessage.Attachments)
                        log.Info("attachment name: "+attachment.Name + "attachment size: " + attachment.Size); 
                }
                //throw new Exception("what is up?");
            }
            catch (Exception ex) 
            {
                log.Error(ex);
            }
        }


        public static void GetEntireDownloadLastMail(string emailserver, string account, string password)
        {
            try
            {
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                log.Info("Begin to receive entire email");
                Pop3 pop3 = new Pop3();
                pop3.Connect(emailserver);
                pop3.Login(account,password);
                MailMessage mailMessage = pop3.DownloadEntireMessage(-1);
                stopwatch.Stop();
                log.Info("End to receive entire email. Time spend secondes: "+ stopwatch.Elapsed.TotalSeconds);
                log.Info("subject: " + mailMessage.Subject);
                log.Debug("if has attachment: " + mailMessage.HasAttachments.ToString());
                log.Debug("mail size: " + mailMessage.Size);
                //log.Info("HTML Body: " + mailMessage.BodyHtmlText);
                //log.Info("Pain Body: " + mailMessage.BodyPlainText);
                string body = string.IsNullOrEmpty(mailMessage.BodyHtmlText) ? mailMessage.BodyPlainText : mailMessage.BodyHtmlText;
                log.Debug("Body: " + body);
                if (mailMessage.HasAttachments)
                {
                    foreach (Attachment attachment in mailMessage.Attachments)
                        log.Debug("attachment name: " + attachment.Name + "attachment size: " + attachment.Size);
                }
                //throw new Exception("what is up?");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public static void GetDownloadEntireMails(string emailserver,string account, string password,int number)
        {
            try
            {
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                Pop3 pop3 = new Pop3();
                pop3.Connect(emailserver);
                pop3.Login(account,password);
                log.Error("Begin to receive entire  emails:");
                MailMessageCollection  mailMessageList = pop3.DownloadEntireMessages(1, number);
                stopwatch.Stop();
                log.Error("End to receive emails, time spend seconds: " + stopwatch.Elapsed.TotalSeconds);
                if (mailMessageList.Count > 0)
                {
                    log.Debug("Received emails: " + mailMessageList.Count);
                    int i = 1;
                    foreach (MailMessage mailmessage in mailMessageList)
                    {
                        log.Info("Email number :"+ i++);
                        log.Info("subject: " + mailmessage.Subject);
                        log.Debug("size: " + mailmessage.Size);
                        string body = string.IsNullOrEmpty(mailmessage.BodyHtmlText) ? mailmessage.BodyPlainText : mailmessage.BodyHtmlText;
                        log.Debug("Body: " + body);
                    }
                }
                else
                {
                    log.Debug("No Emails.");
                } 
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
