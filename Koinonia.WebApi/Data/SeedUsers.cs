using Koinonia.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Koinonia.WebApi.Data
{
    public static class SeedUsers
    {
        private static AppUser user = new AppUser();
        private static IdentityResult result = new IdentityResult();

        public static void SeedData(UserManager<AppUser> userManager)
        {
            SeedUser(userManager);
        }

        private static void SeedUser(UserManager<AppUser> userManager)
        {
            if(userManager.FindByEmailAsync("georgeebisike@gmail.com").Result == null)
            {
                user.Email = "georgeebisike@gmail.com";
                user.UserName = "georgefx";

                //create the new user
                result = userManager.CreateAsync(user).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperAdmin");
                }
            }

            if(userManager.FindByEmailAsync("leadpastor@gamil.com").Result == null)
            {
                user.Email = "leadpastor@gmail.com";
                user.UserName = "leadpastor";

                //create the new user
                result = userManager.CreateAsync(user).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Pastor");
                }
            }
        }
    }
}
