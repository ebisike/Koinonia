using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Koinonia.Application.ViewModels
{
    public class MailMessageObject
    {
        public string SenderAddress { get; set; }
        public string Password { get; set; }
        public string RecieverAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Collection<string> Attachments { get; set; }
    }
}
