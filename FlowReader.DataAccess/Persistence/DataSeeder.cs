using FlowReader.Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace FlowReader.DataAccess.Persistence
{
    public static class DataSeeder
    {
        public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
                foreach (var role in new[] { "User", "Admin" })
                {
                    await roleManager.CreateAsync(new ApplicationRole() { Name = role });
                }
            }

            var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
            if (!adminUsers.Any())
            {
                var adminUser = new ApplicationUser()
                {                   
                    Email = "admin@admin.com",
                    UserName = "admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
