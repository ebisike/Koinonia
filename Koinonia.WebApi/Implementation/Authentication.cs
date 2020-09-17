using Koinonia.Application.HelperClass;
using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Account;
using Koinonia.Domain.Models;
using Koinonia.WebApi.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Koinonia.WebApi.Implementation
{
    public class Authentication : IAuthentication
    {
        private readonly AppSettings _appSetting;
        private readonly IUserService userService;

        public Authentication(IOptions<AppSettings> appSetting, IUserService userService)
        {
            _appSetting = appSetting.Value;
            this.userService = userService;
        }
        public LoginAuthenticationResponse Authenticate(Guid userId, string email, string username)
        {
            KoinoniaUsers user = userService.GetUser(userId);

            //return null if user is not found
            if (user == null) return null;

            //user found successfully, now generate jwt token
            var token = GenerateJwtToken(user);

            return new LoginAuthenticationResponse(user, token, email, username);
        }

        private string GenerateJwtToken(KoinoniaUsers user)
        {
            // generate token that will last for 7days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
