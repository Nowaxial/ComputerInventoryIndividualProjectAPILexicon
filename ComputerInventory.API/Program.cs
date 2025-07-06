using ComputerInventory.API.Extensions;
using ComputerInventory.Core.Repositories;
using ComputerInventory.Data.Data;
using ComputerInventory.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventory.API
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ComputerInventoryContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ComputerInventoryContext") ?? throw new InvalidOperationException("Connection string 'ComputerInventoryContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers(opt => opt.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(ComputerInventoryMappings));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            await app.SeedDataAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}