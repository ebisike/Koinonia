using Koinonia.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koinonia.Application.ViewModels.Account
{
    public class LoginAuthenticationResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public LoginAuthenticationResponse(KoinoniaUsers user, string token, string email, string username)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = email;
            Username = username;
            Token = token;
        }
    }
}
