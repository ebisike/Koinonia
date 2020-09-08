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
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public Collection<string> MailAttachments { get; set; }
        public MyMail(string from, string to, string subject, string body, Collection<string> attachments)
        {
            this.FromAddress = from;
            this.ToAddress = to;
            this.MailSubject = subject;
            this.MailBody = body;
            this.MailAttachments = attachments;
        }
        public MyMail(string from, string to, string subject, string body)
        {
            this.FromAddress = from;
            this.ToAddress = to;
            this.MailSubject = subject;
            this.MailBody = body;
        }

        public bool Send()
        {
            using (MailMessage mailMessage = new MailMessage(FromAddress, ToAddress))
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
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                NetworkCredential networkCredential = new NetworkCredential(FromAddress, "Gr8@gmail");
                smtp.Credentials = networkCredential;



                try
                {
                    smtp.Send(mailMessage);
                    return true;
                }
                catch (Exception e)
                {

                    e.Message.ToString();
                    return false;
                }
            }
        }
    }
}
