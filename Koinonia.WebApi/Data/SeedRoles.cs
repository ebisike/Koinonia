using Koinonia.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Koinonia.WebApi.Data
{
    public static class SeedRoles
    {
        private static IdentityRole role = new IdentityRole();
        public static void SeedData(RoleManager<IdentityRole> roleManager)
        {
            SeedRole(roleManager);
        }

        private static void SeedRole(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("SuperAdmin").Result)
            {
                role.Name = "SuperAdmin";
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Pastor").Result)
            {
                role.Name = "Pastor";
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("user").Result)
            {
                role.Name = "user";
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
