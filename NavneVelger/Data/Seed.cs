using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NavneVelger.Models;
using System;
using System.Threading.Tasks;

namespace NavneVelger.Data
{
    public static class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (string roleName in roleNames)
            {
                // creating the roles and seeding them to the database
                bool roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            string email = "robin.smith83@hotmail.com";
            ApplicationUser adminUser = await userManager.FindByEmailAsync(email);

            if (adminUser == null)
            {
                ApplicationUser administrator = new ApplicationUser();
                administrator.Email = email;
                administrator.UserName = email;

                Task<IdentityResult> newUser = userManager.CreateAsync(administrator, "_AStrongP@ssword!");
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> identityResult = userManager.AddToRoleAsync(administrator, "Admin");
                    identityResult.Wait();
                }
            }
            else
            {
                bool isAdmin = await userManager.IsInRoleAsync(adminUser, "Admin");

                if (!isAdmin)
                {
                    Task<IdentityResult> identityResult = userManager.AddToRoleAsync(adminUser, "Admin");
                    identityResult.Wait();
                }
            }
        }
    }
}
