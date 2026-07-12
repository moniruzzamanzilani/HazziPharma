using HazziPharma.Web.Data;
using Microsoft.AspNetCore.Identity;

namespace HazziPharma.Web.Seed
{
    public static class IdentitySeeder
    {
        public static async Task SeedAdminAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            const string roleName = "Admin";

            const string adminEmail = "admin@hazzipharma.com";

            const string password = "Admin@123";

            // Create Role
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Create Admin User
            var user = await userManager.FindByEmailAsync(adminEmail);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}