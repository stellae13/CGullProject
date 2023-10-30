using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
                    policy.WithOrigins("http://localhost:8000"); 
                });
            });

            builder.Services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ShopContext") ?? throw new InvalidOperationException("Connection string 'ProductContext' not found.")));

            // Add services to the container1.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}