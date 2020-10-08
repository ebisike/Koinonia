using Koinonia.Application.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.ViewModels.Administration
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public List<UsersInRole> RoleUsers { get; set; }
    }
}
