using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
