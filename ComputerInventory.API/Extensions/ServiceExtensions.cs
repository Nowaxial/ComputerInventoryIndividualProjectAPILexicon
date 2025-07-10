using ComputerInventory.Core.Repositories;
using ComputerInventory.Data.Repositories;
using ComputerInventory.Services.Services;
using Service.Contracts.Interfaces;

namespace ComputerInventory.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(builder =>
            {
                builder.AddPolicy("AllowAll", p =>
                p.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
        }
        public static void ConfigureServiceLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IUserService, UserService>();

            services.AddLazy<IInventoryService>();
            services.AddLazy<IUserService>();
           
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddLazy<IInventoryRepository>();
            services.AddLazy<IUserRepository>();
        }


    }
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLazy<TService>(this IServiceCollection services) where TService : class
        {
            return services.AddScoped(provider => new Lazy<TService>(() => provider.GetRequiredService<TService>()));
        }
    }
}


