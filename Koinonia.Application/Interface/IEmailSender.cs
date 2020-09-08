using Koinonia.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.Interface
{
    public interface IEmailSender
    {
        bool SendMail(MailMessageObject mailMessage);
    }
}
