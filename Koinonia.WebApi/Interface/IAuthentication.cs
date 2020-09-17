using Koinonia.Application.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Koinonia.WebApi.Interface
{
    public interface IAuthentication
    {
        LoginAuthenticationResponse Authenticate(Guid userId, string email, string username);
    }
}
