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
                            Name = "food_and_beverage"
                        },
                        new Category
                        {
                            Id = "2",
                            Name = "trinkets"
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
                            Name = "toys_and_stuffed_animals"
                        },
                        new Category
                        {
                            Id = "6",
                            Name = "costumes"
                        },
                        new Category
                        {
                            Id = "7",
                            Name = "health_and_beauty"
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
                        Name = "Seagull Drink",
                        CategoryId = "1",
                        Price = 1.75M,
                        Rating = 2.6M,
                        Stock = 20
                    },
                    new Product
                    {
                        Id = "2",
                        Name = "Seagull Chips",
                        CategoryId = "1",
                        Price = 5.99M,
                        Rating = 4.5M,
                        Stock = 25
                    },
                    new Product
                    {
                        Id = "3",
                        Name = "Seagull Cereal",
                        CategoryId = "1",
                        Price = 69.99M,
                        Rating = 4.2M,
                        Stock = 25
                    },
                    new Product
                    {
                        Id = "4",
                        Name = "Seagull Keychain",
                        CategoryId = "2",
                        Price = 12.99M,
                        Rating = 4.0M,
                        Stock = 30
                    },
                    new Product
                    {
                        Id = "5",
                        Name = "Seagull Action Figure",
                        CategoryId = "2",
                        Price = 50.99M,
                        Rating = 5.0M,
                        Stock = 20
                    },
                    new Product
                    {
                        Id = "6",
                        Name = "Seagull Ornament",
                        CategoryId = "2",
                        Price = 10.99M,
                        Rating = 4.7M,
                        Stock = 50
                    },
                    new Product
                    {
                        Id = "7",
                        Name = "Seagull Hat",
                        CategoryId = "3",
                        Price = 29.99M,
                        Rating = 4.3M,
                        Stock = 12
                    },
                    new Product
                    {
                        Id = "8",
                        Name = "Seagull Sweatshirt",
                        CategoryId = "3",
                        Price = 49.99M,
                        Rating = 4.6M,
                        Stock = 8
                    },
                    new Product
                    {
                        Id = "9",
                        Name = "Seagull T-Shirt",
                        CategoryId = "3",
                        Price = 40.99M,
                        Rating = 4.1M,
                        Stock = 40
                    },
                    new Product
                    {
                        Id = "10",
                        Name = "Seagull Sid Book",
                        CategoryId = "4",
                        Price = 23.49M,
                        Rating = 2.8M,
                        Stock = 50
                    },
                    new Product
                    {
                        Id = "11",
                        Name = "Seagull Soar Book",
                        CategoryId = "4",
                        Price = 21.49M,
                        Rating = 4.8M,
                        Stock = 50
                    },
                    new Product
                    {
                        Id = "12",
                        Name = "Jonathan Livingston Seagull Book",
                        CategoryId = "4",
                        Price = 29.49M,
                        Rating = 2.8M,
                        Stock = 100
                    },
                    new Product
                    {
                        Id = "13",
                        Name = "Seagull Board Game",
                        CategoryId = "5",
                        Price = 25.49M,
                        Rating = 3.8M,
                        Stock = 20
                    }, 
                    new Product
                    {
                        Id = "14",
                        Name = "Seagull Small Plushie",
                        CategoryId = "5",
                        Price = 9.49M,
                        Rating = 4.8M,
                        Stock = 100
                    }, 
                    new Product
                    {
                        Id = "15",
                        Name = "Seagull Big Plushie",
                        CategoryId = "5",
                        Price = 19.49M,
                        Rating = 4.8M,
                        Stock = 100

                    }, 
                    new Product
                    {
                        Id = "16",
                        Name = "Adult Seagull Costume",
                        CategoryId = "6",
                        Price = 119.49M,
                        Rating = 4.8M,
                        Stock = 30
                    }, 
                    new Product
                    {
                        Id = "17",
                        Name = "Child Seagull Costume",
                        CategoryId = "6",
                        Price = 79.49M,
                        Rating = 4.8M,
                        Stock = 35
                    },
                     new Product
                     {
                         Id = "18",
                         Name = "Seagull Hat Costume",
                         CategoryId = "6",
                         Price = 19.49M,
                         Rating = 4.8M,
                         Stock = 35
                     },
                     new Product
                     {
                         Id = "19",
                         Name = "Seagull Makeup Kit",
                         CategoryId = "7",
                         Price = 19.49M,
                         Rating = 4.8M,
                         Stock = 25
                     }
                        );
                }
                //end of product seed data
                context.SaveChanges();

            }
        }
    }
    
}
