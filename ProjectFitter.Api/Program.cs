
using Microsoft.EntityFrameworkCore;
using ProjectFitter.Api.Data;
using ProjectFitter.Api.Data.Extensions;
using ProjectFitter.Api.Helpers;
using System.Reflection;
using FluentValidation;
using ProjectFitter.Api.Services.Abstractions;
using ProjectFitter.Api.Services.Abstractions.DataAccess;
using ProjectFitter.Api.Services.Implementations;
using ProjectFitter.Api.Services.Implementations.DataAccess;
using Microsoft.AspNetCore.Identity;
using ProjectFitter.Api.Data.Entities;
using Microsoft.OpenApi.Models;

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
            services.AddControllers();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IICNumberRepository, ICNumberRepository>();
            services.AddScoped<IICNumberService, ICNumberService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher<Customer>, PasswordHasher<Customer>>();
            services.AddScoped<ISMSService, SMSService>();
            services.AddScoped<IEmailService, EmailService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "KOPERASI TENTERA Mobile API",
                    Description = "An ASP.NET Core Web API for KOPERASI TENTERA",
                });
            });

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
