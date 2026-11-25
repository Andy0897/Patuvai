using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Patuvai.Models;
using Patuvai.Models.Enums;

namespace Patuvai.Data
{
    public class DbInitializer
    {
        private static readonly PasswordHasher<ApplicationUser> _hasher = new();

        public static async Task SeedAdminAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();


            bool exists = await context.ApplicationUsers
                .AnyAsync(u => u.Email == "admin@gmail.com");


            if (exists)
                return;



            var admin = new ApplicationUser
            {
                FirstName = "System",
                LastName = "Administrator",
                Email = "admin@gmail.com",
                PhoneNumber = "0812345678",
                Role = UserRole.Admin
            };


            admin.PasswordHash =
                _hasher.HashPassword(admin, "Pass1234");



            context.ApplicationUsers.Add(admin);


            await context.SaveChangesAsync();
        }

        public static async Task SeedPoliciesAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();


            if (await context.Policies.CountAsync() > 0)
            {
                return;
            }
            else
            {
                var termsPolicy = new Policy
                {
                    Type = PolicyType.Terms,
                    Content = "[Добави съдържание тук]",
                    IsActive = true
                };
                var privacyPolicy = new Policy
                {
                    Type = PolicyType.Privacy,
                    Content = "[Добави съдържание тук]",
                    IsActive = true

                };
                var driverDeclarationPolicy = new Policy
                {
                    Type = PolicyType.DriverDeclaration,
                    Content = "[Добави съдържание тук]",
                    IsActive = true
                };
                context.Policies.AddRange(termsPolicy, privacyPolicy, driverDeclarationPolicy);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedCitiesAsync(ApplicationDbContext context)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "Seed", "cities.json");

            if (File.Exists(filePath))
            {
                var jsonData = await File.ReadAllTextAsync(filePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var cities = JsonSerializer.Deserialize<List<City>>(jsonData, options);

                if (cities != null && cities.Any() && !(await context.Cities.AnyAsync()))
                {
                    await context.Cities.AddRangeAsync(cities);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
