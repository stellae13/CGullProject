using CGullProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CGullProject.Models
{
    public class SeedData
    {
        public static void Initialize( IServiceProvider serviceProvider)
        {
            using(var context = new ShopContext(serviceProvider.GetRequiredService<DbContextOptions<ShopContext>>()))
            {
                
                if (!context.Category.Any())
                {
                    context.Category.AddRange(

                        new Category
                        {
                            Id = "1",
                            Name = "sporting_goods"
                        },
                        new Category
                        {
                            Id = "2",
                            Name = "electronics"
                        },
                        new Category
                        {
                            Id = "3",
                            Name = "clothing"
                        },
                        new Category
                        {
                            Id = "4",
                            Name = "books"
                        },
                        new Category
                        {
                            Id = "5",
                            Name = "toys"
                        },
                        new Category
                        {
                            Id = "6",
                            Name = "automotive"
                        },
                        new Category
                        {
                            Id = "7",
                            Name = "home_appliances"
                        },
                        new Category
                        {
                            Id = "8",
                            Name = "furniture"
                        },
                        new Category
                        {
                            Id = "9",
                            Name = "health_and_beauty"
                        },
                        new Category
                        {
                            Id = "10",
                            Name = "food_and_beverage"
                        }
                    
                        );

                }
                // end of category seed data 
                if(!context.Product.Any())
                {
                    context.AddRange(
                    new Product
                    {
                        Id = "1",
                        Name = "Coke",
                        CategoryId = "10",
                        Price = 1.4M,
                        Rating = 2.6M,
                        Stock = 15
                    },
                    new Product
                    {
                        Id = "2",
                        Name = "Laptop",
                        CategoryId = "2",
                        Price = 899.99M,
                        Rating = 4.5M,
                        Stock = 10
                    },
                    new Product
                    {
                        Id = "3",
                        Name = "Running Shoes",
                        CategoryId = "1",
                        Price = 69.99M,
                        Rating = 4.2M,
                        Stock = 25
                    },
                    new Product
                    {
                        Id = "4",
                        Name = "Fiction Book",
                        CategoryId = "4",
                        Price = 12.99M,
                        Rating = 4.0M,
                        Stock = 30
                    },
                    new Product
                    {
                        Id = "5",
                        Name = "Action Figure",
                        CategoryId = "5",
                        Price = 14.99M,
                        Rating = 3.8M,
                        Stock = 20
                    },
                    new Product
                    {
                        Id = "6",
                        Name = "Car Oil",
                        CategoryId = "6",
                        Price = 8.99M,
                        Rating = 4.7M,
                        Stock = 50
                    },
                    new Product
                    {
                        Id = "7",
                        Name = "Microwave",
                        CategoryId = "7",
                        Price = 129.99M,
                        Rating = 4.3M,
                        Stock = 12
                    },
                    new Product
                    {
                        Id = "8",
                        Name = "Dining Table",
                        CategoryId = "8",
                        Price = 249.99M,
                        Rating = 4.6M,
                        Stock = 8
                    },
                    new Product
                    {
                        Id = "9",
                        Name = "Shampoo",
                        CategoryId = "9",
                        Price = 5.99M,
                        Rating = 4.1M,
                        Stock = 40
                    },
                    new Product
                    {
                        Id = "10",
                        Name = "Chocolate",
                        CategoryId = "10",
                        Price = 2.49M,
                        Rating = 4.8M,
                        Stock = 100
                    }
                        );
                }
                //end of product seed data
                context.SaveChanges();

            }
        }
    }
    
}
