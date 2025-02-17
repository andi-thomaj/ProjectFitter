
using Microsoft.EntityFrameworkCore;
using ProjectFitter.Api.Data;
using ProjectFitter.Api.Data.Extensions;
using ProjectFitter.Api.Helpers;

namespace ProjectFitter.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;
            // Add services to the container.

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Ensure.NotNullOrEmpty(connectionString);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                await app.Services.InitializeDbAsync();
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
