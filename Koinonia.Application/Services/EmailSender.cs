using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.Services
{
    public class EmailSender : IEmailSender
    {
        public bool SendMail(MailMessageObject mailMessage)
        {
            MyMail emailService = null;
            if(mailMessage.Attachments != null)
            {
                emailService = new MyMail(mailMessage.SenderAddress, mailMessage.RecieverAddress, mailMessage.Subject, mailMessage.Body, mailMessage.Attachments);
            }
            else
            {
                 emailService = new MyMail(mailMessage.SenderAddress, mailMessage.RecieverAddress, mailMessage.Subject, mailMessage.Body);
            }

            if (emailService.Send())
            {
                return true;
            }
            return false;
        }
    }
}
