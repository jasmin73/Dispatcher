using Dispatcher.DataAccess;
using Dispatcher.DataAccess.Data;
using Microsoft.AspNetCore.Identity;

namespace Dispatcher.Server.Helper
{
    public static class DbInitializer
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // 1. Seed Tenant
            var tenant = context.Tenants.FirstOrDefault(t => t.Name == "Picerija Mare");
            if (tenant == null)
            {
                tenant = new Tenant
                {
                    Name = "Pizzeria Jimmy",
                    Street = "Zaplanjska ",
                    Number = "57",
                    City = "Beograd",
                    Latitude = null,
                    Longitude = null
                };
                context.Tenants.Add(tenant);
                await context.SaveChangesAsync();
            }

            // 2. Seed User
            var email = "jasmin@picerija.com";
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    FullName = "Jasmin Operater",
                    TenantId = tenant.Id
                };
                await userManager.CreateAsync(user, "Test123!");
            }
        }
    }

}
