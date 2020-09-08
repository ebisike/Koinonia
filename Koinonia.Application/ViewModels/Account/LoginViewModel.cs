using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Koinonia.Application.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        public string username { get; set; }
        public string Password { get; set; }
    }
}
