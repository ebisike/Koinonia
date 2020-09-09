using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Koinonia.Application.Interface;
using Koinonia.Application.ViewModels.Profile;
using Koinonia.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Koinonia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly UserManager<AppUser> userManager;

        public ProfileController(IUserService userService,
            UserManager<AppUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        //GET: api/Profile
        [HttpGet]
        [Route("Home")]
        public async Task<IActionResult> Home(Guid userId)
        {
            if(userId != null)
            {
                var profile = userService.GetUserProfile(userId);

                //get the appuser object
                var appuser = await GetUser();
                profile.Email = appuser.Email;
                profile.Username = appuser.UserName;
                profile.PhoneNumber = appuser.PhoneNumber;

                return Ok(profile);
            }
            return BadRequest(new { message = "Id cannot be null" });
        }

        //GET: api/Profile
        [HttpGet]
        [Route("EditProfile")]
        public IActionResult EditProfile()
        {
            //get the currently login user
            var appUser = GetUser().Result;
            var profile = userService.GetUser(Guid.Parse(appUser.Id));
            return Ok(profile);
        }

        //PUT: api/Profile
        [HttpPut]
        [Route("EditProfile")]
        public async Task<IActionResult> EditProfile(EditprofileViewModel model)
        {
            if(model != null)
            {
                //get the current login user to update his profile
                var appUser = await GetUser();
                model.Id = Guid.Parse(appUser.Id);

                var result = await userService.UpdateProfile(model);
                if(result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest(new { message = "model is null" });
        }

        private async Task<AppUser> GetUser()
        {
            return await userManager.GetUserAsync(User);
        }
    }
}
