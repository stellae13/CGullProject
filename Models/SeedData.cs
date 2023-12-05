using CGullProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CGullProject.Models
{
    /// <summary>
    /// Contains data that will populate the database for testing purposes.
    /// </summary>
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ShopContext(serviceProvider.GetRequiredService<DbContextOptions<ShopContext>>()))
            {

                if (!context.Category.Any())
                {
                    context.Category.AddRange(

                        new Category
                        {
                            Name = "food_and_beverage",

                        },
                        new Category
                        {
                            Name = "trinkets",

                        },
                        new Category
                        {
                            Name = "clothing",

                        },
                        new Category
                        {
                            Name = "books",

                        },
                        new Category
                        {
                            Name = "toys_and_stuffed_animals",

                        },
                        new Category
                        {
                            Name = "costumes",

                        },
                        new Category
                        {
                            Name = "health_and_beauty",

                        },
                        new Category
                        {
                            Name = "misc",

                        }

                        );

                }
                // end of category seed data 
                if (!context.Inventory.Any())
                {
                    context.Inventory.AddRange(
                    new Item
                    {
                        Id = "000001",
                        Name = "Seagull Drink",
                        CategoryId = 1,
                        MSRP = 1.75M,
                        SalePrice = 1.60M,
                        Rating = 0.0M,
                        Stock = 20,
                        IsBundle = false,
                        OnSale = true
                    },
                    new Item
                    {
                        Id = "000002",
                        Name = "Seagull Chips",
                        CategoryId = 1,
                        SalePrice = 5.80M,
                        MSRP = 5.99M,
                        Rating = 4.5M,
                        Stock = 25,
                        IsBundle = false,
                        OnSale = false

                    },
                    new Item
                    {
                        Id = "000003",
                        Name = "Seagull Cereal",
                        CategoryId = 1,
                        MSRP = 69.99M,
                        SalePrice = 67.99M,
                        Rating = 4.2M,
                        Stock = 25,
                        IsBundle = false,
                        OnSale = true

                    },
                    new Item
                    {
                        Id = "000004",
                        Name = "Seagull Keychain",
                        CategoryId = 2,
                        MSRP = 12.99M,
                        SalePrice = 11.99M,
                        Rating = 4.0M,
                        Stock = 30,
                        IsBundle = false,
                        OnSale = false

                    },
                    new Item
                    {
                        Id = "000005",
                        Name = "Seagull Action Figure",
                        CategoryId = 2,
                        MSRP = 50.99M,
                        SalePrice = 47.99M,
                        Rating = 5.0M,
                        Stock = 20,
                        IsBundle = false,
                        OnSale = true

                    },
                    new Item
                    {
                        Id = "000006",
                        Name = "Seagull Ornament",
                        CategoryId = 2,
                        MSRP = 10.99M,
                        SalePrice = 8.99M,
                        Rating = 4.7M,
                        Stock = 50,
                        IsBundle = false,
                        OnSale = true

                    },
                    new Item
                    {
                        Id = "000007",
                        Name = "Seagull Hat",
                        CategoryId = 3,
                        MSRP = 29.99M,
                        SalePrice = 28.99M,
                        Rating = 4.3M,
                        Stock = 12,
                        IsBundle = false,
                        OnSale = false

                    },
                    new Item
                    {
                        Id = "000008",
                        Name = "Seagull Sweatshirt",
                        CategoryId = 3,
                        MSRP = 49.99M,
                        SalePrice = 47.99M,
                        Rating = 4.6M,
                        Stock = 8,
                        IsBundle = false,
                        OnSale = true

                    },
                    new Item
                    {
                        Id = "000009",
                        Name = "Seagull T-Shirt",
                        CategoryId = 3,
                        MSRP = 40.99M,
                        SalePrice = 39.99M,
                        Rating = 4.1M,
                        Stock = 40,
                        IsBundle = false,
                        OnSale = false

                    },
                    new Item
                    {
                        Id = "000010",
                        Name = "Seagull Sid Book",
                        CategoryId = 4,
                        MSRP = 23.49M,
                        SalePrice = 22.49M,
                        Rating = 2.8M,
                        Stock = 50,
                        IsBundle = false,
                        OnSale = true

                    },
                    new Item
                    {
                        Id = "000011",
                        Name = "Seagull Soar Book",
                        CategoryId = 4,
                        MSRP = 21.49M,
                        SalePrice = 20.49M,
                        Rating = 4.8M,
                        Stock = 50,
                        IsBundle = false,
                        OnSale = true

                    },
                    new Item
                    {
                        Id = "000012",
                        Name = "Jonathan Livingston Seagull Book",
                        CategoryId = 4,
                        MSRP = 29.49M,
                        SalePrice = 28.49M,
                        Rating = 2.8M,
                        Stock = 100,
                        IsBundle = false,
                        OnSale = true

                    },
                    new Item
                    {
                        Id = "000013",
                        Name = "Seagull Board Game",
                        CategoryId = 5,
                        MSRP = 25.49M,
                        SalePrice = 24.49M,
                        Rating = 3.8M,
                        Stock = 20,
                        IsBundle = false,
                        OnSale = true

                    },
                    new Item
                    {
                        Id = "000014",
                        Name = "Seagull Small Plushie",
                        CategoryId = 5,
                        MSRP = 9.49M,
                        SalePrice = 8.49M,
                        Rating = 4.8M,
                        Stock = 100,
                        IsBundle = false,
                        OnSale = false

                    },
                    new Item
                    {
                        Id = "000015",
                        Name = "Seagull Big Plushie",
                        CategoryId = 5,
                        MSRP = 19.49M,
                        SalePrice = 18.49M,
                        Rating = 4.8M,
                        Stock = 100,
                        IsBundle = false,
                        OnSale = true


                    },
                    new Item
                    {
                        Id = "000016",
                        Name = "Adult Seagull Costume",
                        CategoryId = 6,
                        MSRP = 119.49M,
                        SalePrice = 109.49M,
                        Rating = 4.8M,
                        Stock = 30,
                        IsBundle = false,
                        OnSale = false

                    },
                    new Item
                    {
                        Id = "000017",
                        Name = "Child Seagull Costume",
                        CategoryId = 6,
                        MSRP = 79.49M,
                        SalePrice = 69.49M,
                        Rating = 4.8M,
                        Stock = 35,
                        IsBundle = false,
                        OnSale = true

                    },
                     new Item
                     {
                         Id = "000018",
                         Name = "Seagull Hat Costume",
                         CategoryId = 6,
                         MSRP = 19.49M,
                         SalePrice = 18.49M,
                         Rating = 4.8M,
                         Stock = 35,
                         IsBundle = false,
                         OnSale = true

                     },
                     new Item
                     {
                         Id = "000019",
                         Name = "Seagull Makeup Kit",
                         CategoryId = 7,
                         MSRP = 19.49M,
                         SalePrice = 18.49M,
                         Rating = 4.8M,
                         Stock = 25,
                         IsBundle = false,
                         OnSale = false

                     },
                     new Item
                     {
                         Id = "000020",
                         Name = "Sounds of the Gull",
                         CategoryId = 7,
                         MSRP = 18.99M,
                         SalePrice = 16.99M,
                         Rating = 4.9M,
                         Stock = 35,
                         IsBundle = false,
                         OnSale = true

                     },
                     new Item
                     {
                         Id = "100021",
                         Name = "Costume Bundle",
                         CategoryId = 6,
                         MSRP = 189.99M,
                         SalePrice = 179.99M,
                         Rating = 4.9M,
                         Stock = 1,
                         IsBundle = true,
                         OnSale = false

                     },
                     new Item
                     {
                         Id = "000023",
                         Name = "Seagull Teapot",
                         CategoryId = 2,
                         MSRP = 99.99M,
                         SalePrice = 89.99M,
                         Rating = 4.9M,
                         Stock = 40,
                         IsBundle = false,
                         OnSale = true

                     },
                     new Item
                     {
                         Id = "000024",
                         Name = "Seagull 3D Phone Case",
                         CategoryId = 8,
                         MSRP = 29.49M,
                         SalePrice = 19.49M,
                         Rating = 3.8M,
                         Stock = 25,
                         IsBundle = false,
                         OnSale = false

                     },
                     new Item
                     {
                         Id = "000025",
                         Name = "Seagull Painted Case",
                         CategoryId = 8,
                         MSRP = 17.99M,
                         SalePrice = 15.99M,
                         Rating = 4.5M,
                         Stock = 35,
                         IsBundle = false,
                         OnSale = false

                     },
                     new Item
                     {
                         Id = "000026",
                         Name = "Seagull Jean Case",
                         CategoryId = 8,
                         MSRP = 15.99M,
                         SalePrice = 10.99M,
                         Rating = 4.9M,
                         Stock = 45,
                         IsBundle = false,
                         OnSale = true

                     },
                     new Item
                     {
                         Id = "000027",
                         Name = "Seagull Mug",
                         CategoryId = 1,
                         MSRP = 17.99M,
                         SalePrice = 15.99M,
                         Rating = 4.2M,
                         Stock = 50,
                         IsBundle = false,
                         OnSale = true

                     },
                     new Item
                     {
                         Id = "100028",
                         Name = "Seagull Plushies",
                         CategoryId = 5,
                         MSRP = 17.99M,
                         SalePrice = 15.99M,
                         Rating = 4.2M,
                         Stock = 1,
                         IsBundle = true,
                         OnSale = false
                     },
                     new Item
                     {
                         Id = "000029",
                         Name = "Decorative Seagull License Plate",
                         CategoryId = 2,
                         MSRP = 37.99M,
                         SalePrice = 35.99M,
                         Rating = 4.2M,
                         Stock = 20,
                         IsBundle = false,
                         OnSale = true

                     },
                     new Item { 
                         Id = "000030",
                         Name = "Authentic Canned Seagull Meat",
                         CategoryId = 1,
                         MSRP = 300M,
                         SalePrice = 2M,
                         Rating = 1M,
                         Stock = 100,
                         IsBundle = false,
                         OnSale = true
                     },
                     new Item
                     {
                         Id = "000031",
                         Name = "Armored Seagull Figurine",
                         CategoryId = 2,
                         MSRP = 30M,
                         SalePrice = 29M,
                         Rating = 1M,
                         Stock = 100,
                         IsBundle = false,
                         OnSale = true
                     }
                     );
                }
                //end of product seed data
                if (!context.Cart.Any())
                {
                    context.AddRange(

                        new Cart()
                        {
                            Id = Guid.Parse("c0c2ce33-81a5-4f6d-88e2-419d41e135fb"),
                            Name = "Ryan"
                        },
                        new Cart()
                        {
                            Id = Guid.Parse("2c6dce67-a449-4eab-b4df-1641172fbd92"),
                            Name = "Stella"
                        },
                        new Cart()
                        {
                            Id = Guid.Parse("8df2e0f0-4bf0-41e5-ab9d-c7fd82842bdf"),
                            Name = "Burt"
                        },
                        new Cart()
                        {
                            Id = Guid.Parse("7a2066e6-b9c6-4ccb-8b20-38837d54a45b"),
                            Name = "Brent"
                        }
                    );
                }
                // end of Cart seed data

                if (!context.Bundle.Any())
                {
                    context.AddRange(

                        new Bundle()
                        {
                            ProductId = "100020",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now + TimeSpan.FromDays(100)

                        },
                        new Bundle()
                        {
                            ProductId = "100022",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now + TimeSpan.FromDays(100)

                        },
                        new Bundle()
                        {
                            ProductId = "100028",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now + TimeSpan.FromDays(100)

                        }
                    );

                }
                // end of Bundle seed data
                if (!context.BundleItem.Any())
                {
                    context.AddRange(

                        new BundleItem()
                        {
                            BundleId = "100020",
                            ProductId = "000016"

                        },
                        new BundleItem()
                        {
                            BundleId = "100020",
                            ProductId = "000017"

                        },
                        new BundleItem()
                        {
                            BundleId = "100022",
                            ProductId = "000010"

                        },
                        new BundleItem()
                        {
                            BundleId = "100022",
                            ProductId = "000011"

                        },
                        new BundleItem()
                        {
                            BundleId = "100022",
                            ProductId = "000012"

                        },
                        new BundleItem()
                        {
                            BundleId = "100028",
                            ProductId = "000014"

                        },
                        new BundleItem()
                        {
                            BundleId = "100028",
                            ProductId = "000015"

                        }
                    );

                }

                if (!context.Admins.Any()) {
                    using (SHA256 shaCtx = SHA256.Create()) {
                        byte[] defPassHash = shaCtx.ComputeHash(Encoding.UTF8.GetBytes("password"));
                        context.AddRange(
                            new Admins()
                            {
                                Username = "stellagarcia",
                                Password = defPassHash
                            },
                            new Admins()
                            {
                                Username = "manager",
                                Password = defPassHash
                            },
                            new Admins()
                            {
                                Username = "thatmanryan",
                                Password = shaCtx.ComputeHash(Encoding.UTF8.GetBytes("otto_are_you_married?"))
                            },
                            new Admins()
                            {
                                Username = "o's_husband_44",
                                Password = defPassHash
                            }
                        );
                    }
                }
                context.SaveChanges();

            }
        }
    }

}
