﻿using Microsoft.EntityFrameworkCore;
using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Services;
using CGullProject.Services.ServiceInterfaces;

namespace CGullProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var CGullAllowSpecificOrigins = "_cgullAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CGullAllowSpecificOrigins, policy => {
                    policy.WithOrigins(new string[2] { 
                        "http://localhost:8000",  // Allow local FE host
                        "http://cgulls.ddns.net"  // Allow remote FE host
                    });
                    // Additional policy change, allows any origin to make state-altering http req's like POST, PUT, etc
                    policy.AllowAnyHeader();
                    
                });
            });

            builder.Services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ShopContext") ?? throw new InvalidOperationException("Connection string 'ProductContext' not found.")));


            // Add services to the container1.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IInventoryService, InventoryService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(CGullAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}