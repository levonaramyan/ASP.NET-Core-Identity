using Identity_Basic_Example.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Basic_Example.Helpers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "qwerty@test.com";
            string password = "Qwerty123!";

            if(await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                User applicationAdmin = new User { Email = adminEmail, UserName = adminEmail };

                var result = await userManager.CreateAsync(applicationAdmin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(applicationAdmin,"admin");
                }
            }
        }
    }
}
