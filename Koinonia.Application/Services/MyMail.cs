using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Koinonia.Application.Services
{
    public class MyMail
    {
        public string SenderAddress { get; set; }
        public string RecieverAddress { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string Password { get; set; }
        public Collection<string> MailAttachments { get; set; }
        public MyMail(string SenderAddress, string password, string RecieverAddress, string subject, string body, Collection<string> attachments)
        {
            this.SenderAddress = SenderAddress;
            this.Password = password;
            this.RecieverAddress = RecieverAddress;
            this.MailSubject = subject;
            this.MailBody = body;
            this.MailAttachments = attachments;
        }
        public MyMail(string SenderAddress, string password, string RecieverAddress, string subject, string body)
        {
            this.SenderAddress = SenderAddress;
            this.Password = password;
            this.RecieverAddress = RecieverAddress;
            this.MailSubject = subject;
            this.MailBody = body;
        }

        public bool Send()
        {
            using (MailMessage mailMessage = new MailMessage(SenderAddress, RecieverAddress))
            {
                mailMessage.Subject = MailSubject;
                mailMessage.Body = MailBody;
                //check if there are attachments
                if (MailAttachments.Count > 0)
                {
                    foreach (var item in MailAttachments)
                    {
                        //loop through each attachments
                        mailMessage.Attachments.Add(new Attachment(item));
                    }
                }

                mailMessage.IsBodyHtml = true;

                //create Smtp client
                SmtpClient smtp = new SmtpClient();

                //to determine the sender host, we check which email service provider the sender address is using
                string[] host = SenderAddress.Split('@');
                smtp.Host = null;
                switch (host[1])
                {
                    case "gmail.com":
                        smtp.Host = "smtp.gmail.com";
                        break;
                    case "yahoo.com":
                        smtp.Host = "smtp.mail.yahoo.com";
                        break;
                    case "live.com":
                        smtp.Host = "smtp.live.com";
                        break;
                    default:
                        break;
                }
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                NetworkCredential networkCredential = new NetworkCredential(SenderAddress, Password);
                smtp.Credentials = networkCredential;



                try
                {
                    smtp.Send(mailMessage);
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message.ToString());
                }
            }
        }
    }
}
