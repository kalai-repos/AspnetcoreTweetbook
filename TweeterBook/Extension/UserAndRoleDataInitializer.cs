using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweeterBook.Extension
{
    public static class UserAndRoleDataInitializer  
    {

        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("johndoe@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "johndoe@localhost";
                user.Email = "johndoe@localhost";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            if (userManager.FindByEmailAsync("alex@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "alex@localhost";
                user.Email = "alex@localhost";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "User"
                };
                _ = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                _ = roleManager.
                CreateAsync(role).Result;
            }
        }

    }
}
