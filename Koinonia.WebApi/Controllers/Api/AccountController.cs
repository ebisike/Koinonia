using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Koinonia.Application.HelperClass;
using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels;
using Koinonia.Application.ViewModels.Account;
using Koinonia.Domain.Models;
using Koinonia.WebApi.Interface;
using Koinonia.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Koinonia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppSettings _appSettings;
        private readonly IAuthentication _authentication;
        private readonly IUserService userService;
        private readonly IEmailSender emailSender;
        private readonly IFollowService followService;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> appSettings,
            IAuthentication authentication,
            IUserService userService,
            IEmailSender emailSender,
            IFollowService followService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _appSettings = appSettings.Value;
            _authentication = authentication;
            this.userService = userService;
            this.emailSender = emailSender;
            this.followService = followService;
        }

        private async Task<AppUser> GetUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        //POST: api/Account
        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(RegistrationViewModel model)
        {
            try
            {
                //verify that email or username is not in use
                if(await userManager.FindByNameAsync(model.Username) == null && await userManager.FindByEmailAsync(model.Email) == null)
                {
                    var AppUser = new AppUser()
                    {
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        UserName = model.Username
                    };

                    var result = await userManager.CreateAsync(AppUser, model.Password);
                    if (result.Succeeded)
                    {
                        //get the AppUser by Id
                        var CreatedUser = GetUserByEmail(model.Email).Result;

                        //assign the user to a role
                        var roleResult = await userManager.AddToRoleAsync(CreatedUser, "user");
                        if (roleResult.Succeeded)
                        {
                            //build the koinonia user entity
                            KoinoniaUserModel koinoniaUsers = new KoinoniaUserModel()
                            {
                                Id = Guid.Parse(CreatedUser.Id),
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Gender = model.Gender,
                                stateOfOrigin = model.StateOfOrigin,
                            };
                            //pass the model to the UserService
                            userService.AddNewUser(koinoniaUsers).Wait();

                            //call method to auto follow the leadpastor
                            AutoFollow(koinoniaUsers.Id).Wait();
                            return Ok(new { message = "Your Account has been created succesfully"});
                        }
                        return BadRequest("User was not added to a role");
                    }
                    return BadRequest("Registration Failed");
                }
                return BadRequest("Username and/or Email is Already in use");
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<bool> AutoFollow(Guid memberId)
        {
            //first we get the leadpastor by email
            var pastor = userManager.FindByEmailAsync("leadpastor@gmail.com").Result;
            if(pastor != null)
            {
                var follwership = await followService.FollowUser(memberId, Guid.Parse(pastor.Id));

                if(follwership != null)
                {
                    return true;
                }
            }
            return false;
        }

        //POST: api/Account
        [HttpPost]
        [Route("Signin")]
        public async Task<IActionResult> Signin(LoginViewModel model)
        {
            //validate that the provided username is registered on the app
            var user = await userManager.FindByNameAsync(model.username);
            if(user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var response = _authentication.Authenticate(Guid.Parse(user.Id), user.Email, user.UserName);
                    if(response == null)
                        return BadRequest(new { message = "Username or password is incorrect" });
                    

                    //get the user role
                    string userRole = await GetUserRole(user);
                    return Ok(new { User = response, role = userRole});
                }
                return BadRequest("Sorry Username and password do not match");
            }
            return BadRequest(new { message = "Sorry the username provide is not registered on this platform" });
        }

        private async Task<string> GetUserRole(AppUser User)
        {
            foreach (var role in roleManager.Roles)
            {
                if(await userManager.IsInRoleAsync(User, role.Name))
                {
                    return role.Name;
                }
            }

            return "No Role for the User";
        }

        [HttpGet]
        [Route("Signout")]
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { message = "Signout successful" });
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            //find the user by email
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user != null)
            {
                //generate password reset token
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);

                //build the url
                var url = Url.Action("ResetPassword", "Account", new { email = user.Email, token = validToken }, Request.Scheme);

                //send email to the user
                var mail = new MailMessageObject()
                {
                    SenderAddress = "georgefx.creativecompany@gamil.com",
                    RecieverAddress = user.Email,
                    Subject = "Passeord Reset",
                    Body = url
                };
                emailSender.SendMail(mail);
                return Ok(new { messge = "Password Reset Link sent to specified email address", link = url });
            }
            return BadRequest(new { message = "Sorry User not found" });
        }

        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string email, string token)
        {
            if(email != null && token != null)
            {
                var resetmodel = new ResetPasswordViewModel()
                {
                    Email = email,
                    token = token,
                };
                return Ok(resetmodel);
            }
            return BadRequest(new { message = "Sorry Email or Token cannot be null" });
        }

        //POST: api/Account
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPasword(ResetPasswordViewModel model)
        {
            if(model.Password != null || model.Email != null || model.token != null)
            {
                //find the user by email
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    return NotFound();
                }

                //decode the token
                var decodedToken = WebEncoders.Base64UrlDecode(model.token);
                var normalToken = Encoding.UTF8.GetString(decodedToken);

                //reset the password
                var result = await userManager.ResetPasswordAsync(user, normalToken, model.Password);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Password rest succesful" });
                }
                return BadRequest("An error occured.");
            }
            return BadRequest(new { message = "Model cannot be empty" });
        }
    }
}
