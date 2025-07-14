using ComputerInventory.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventory.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<ComputerInventoryContext>();
            await db.Database.MigrateAsync();

            try
            {
                await SeedData.Initialize(db);
            }
            catch (Exception ex)
            {
                // Logga eventuellt fel här
                Console.WriteLine($"Fel vid seeding: {ex.Message}");
            }
        }
    }
}