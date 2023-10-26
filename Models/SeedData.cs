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
                if(!context.Inventory.Any())
                {
                    context.AddRange(
                    new Inventory
                    {
                        Id = "000001",
                        Name = "Seagull Drink",
                        CategoryId = 1,
                        MSRP = 1.75M,
                        SalePrice = 1.75M,
                        Rating = 2.6M,
                        Stock = 20
                    },
                    new Inventory
                    {
                        Id = "000002",
                        Name = "Seagull Chips",
                        CategoryId = 1,
                        SalePrice = 5.99M,
                        MSRP = 5.99M,
                        Rating = 4.5M,
                        Stock = 25
                    },
                    new Inventory
                    {
                        Id = "000003",
                        Name = "Seagull Cereal",
                        CategoryId = 1,
                        MSRP = 69.99M,
                        SalePrice = 69.99M,
                        Rating = 4.2M,
                        Stock = 25
                    },
                    new Inventory
                    {
                        Id = "000004",
                        Name = "Seagull Keychain",
                        CategoryId = 2,
                        MSRP = 12.99M,
                        SalePrice = 12.99M,
                        Rating = 4.0M,
                        Stock = 30
                    },
                    new Inventory
                    {
                        Id = "000005",
                        Name = "Seagull Action Figure",
                        CategoryId = 2,
                        MSRP = 50.99M,
                        SalePrice = 50.99M,
                        Rating = 5.0M,
                        Stock = 20
                    },
                    new Inventory
                    {
                        Id = "000006",
                        Name = "Seagull Ornament",
                        CategoryId = 2,
                        MSRP = 10.99M,
                        SalePrice = 10.99M,
                        Rating = 4.7M,
                        Stock = 50
                    },
                    new Inventory
                    {
                        Id = "000007",
                        Name = "Seagull Hat",
                        CategoryId = 3,
                        MSRP = 29.99M,
                        SalePrice = 29.99M,
                        Rating = 4.3M,
                        Stock = 12
                    },
                    new Inventory
                    {
                        Id = "000008",
                        Name = "Seagull Sweatshirt",
                        CategoryId = 3,
                        MSRP = 49.99M,
                        SalePrice = 49.99M,
                        Rating = 4.6M,
                        Stock = 8
                    },
                    new Inventory
                    {
                        Id = "000009",
                        Name = "Seagull T-Shirt",
                        CategoryId = 3,
                        MSRP = 40.99M,
                        SalePrice = 40.99M,
                        Rating = 4.1M,
                        Stock = 40
                    },
                    new Inventory
                    {
                        Id = "000010",
                        Name = "Seagull Sid Book",
                        CategoryId = 4,
                        MSRP = 23.49M,
                        SalePrice = 23.49M,
                        Rating = 2.8M,
                        Stock = 50
                    },
                    new Inventory
                    {
                        Id = "000011",
                        Name = "Seagull Soar Book",
                        CategoryId = 4,
                        MSRP = 21.49M,
                        SalePrice = 21.49M,
                        Rating = 4.8M,
                        Stock = 50
                    },
                    new Inventory
                    {
                        Id = "000012",
                        Name = "Jonathan Livingston Seagull Book",
                        CategoryId = 4,
                        MSRP = 29.49M,
                        SalePrice = 29.49M,
                        Rating = 2.8M,
                        Stock = 100
                    },
                    new Inventory
                    {
                        Id = "000013",
                        Name = "Seagull Board Game",
                        CategoryId = 5,
                        MSRP = 25.49M,
                        SalePrice = 25.49M,
                        Rating = 3.8M,
                        Stock = 20
                    }, 
                    new Inventory
                    {
                        Id = "000014",
                        Name = "Seagull Small Plushie",
                        CategoryId = 5,
                        MSRP = 9.49M,
                        SalePrice = 9.49M,
                        Rating = 4.8M,
                        Stock = 100
                    }, 
                    new Inventory
                    {
                        Id = "000015",
                        Name = "Seagull Big Plushie",
                        CategoryId = 5,
                        MSRP = 19.49M,
                        SalePrice = 19.49M,
                        Rating = 4.8M,
                        Stock = 100

                    }, 
                    new Inventory
                    {
                        Id = "000016",
                        Name = "Adult Seagull Costume",
                        CategoryId = 6,
                        MSRP = 119.49M,
                        SalePrice = 119.49M,
                        Rating = 4.8M,
                        Stock = 30
                    }, 
                    new Inventory
                    {
                        Id = "000017",
                        Name = "Child Seagull Costume",
                        CategoryId = 6,
                        MSRP = 79.49M,
                        SalePrice = 79.49M,
                        Rating = 4.8M,
                        Stock = 35
                    },
                     new Inventory
                     {
                         Id = "000018",
                         Name = "Seagull Hat Costume",
                         CategoryId = 6,
                         MSRP = 19.49M,
                         SalePrice = 19.49M,
                         Rating = 4.8M,
                         Stock = 35
                     },
                     new Inventory
                     {
                         Id = "000019",
                         Name = "Seagull Makeup Kit",
                         CategoryId = 7,
                         MSRP = 19.49M,
                         SalePrice = 19.49M,
                         Rating = 4.8M,
                         Stock = 25
                     },
                     new Inventory
                     {
                         Id = "000020",
                         Name = "Sounds of the Gull",
                         CategoryId = 7,
                         MSRP = 18.99M,
                         SalePrice = 18.99M,
                         Rating = 4.9M,
                         Stock = 35
                     },
                     new Inventory
                     {
                         Id = "100021",
                         Name = "Costume Bundle",
                     },
                     new Inventory
                     {
                         Id = "100022",
                         Name = "Costume Bundle with Makeup",
                     },
                     new Inventory
                     {
                         Id = "000023",
                         Name = "Seagull Teapot",
                         CategoryId = 2,
                         MSRP = 99.99M,
                         SalePrice = 89.99M,
                         Rating = 4.9M,
                         Stock = 40
                     },
                     new Inventory
                     {
                         Id = "000024",
                         Name = "Seagull 3D Phone Case",
                         CategoryId = 8,
                         MSRP = 29.49M,
                         SalePrice = 19.49M,
                         Rating = 3.8M,
                         Stock = 25
                     },
                     new Inventory
                     {
                         Id = "000025",
                         Name = "Seagull Painted Case",
                         CategoryId = 8,
                         MSRP = 17.99M,
                         SalePrice = 15.99M,
                         Rating = 4.5M,
                         Stock = 35
                     },
                     new Inventory
                     {
                         Id = "000026",
                         Name = "Seagull Jean Case",
                         CategoryId = 8,
                         MSRP = 15.99M,
                         SalePrice = 10.99M,
                         Rating = 4.9M,
                         Stock = 45
                     },
                     new Inventory
                     {
                         Id = "000027",
                         Name = "Seagull Mug",
                         CategoryId = 1,
                         MSRP = 17.99M,
                         SalePrice = 15.99M,
                         Rating = 4.2M,
                         Stock = 50
                     },
                     new Inventory
                     {
                         Id = "100028",
                         Name = "Seagull Plushies"
                     },
                     new Inventory
                     {
                         Id = "000029",
                         Name = "Decorative Seagull License Plate",
                         CategoryId = 2,
                         MSRP = 37.99M,
                         SalePrice = 35.99M,
                         Rating = 4.2M,
                         Stock = 20
                     }

                     );
                }
                //end of product seed data
                if (!context.Cart.Any())
                {
                    context.AddRange(
                    
                        new Cart()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Ryan"
                        },
                        new Cart()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Stella"
                        },
                        new Cart()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Burt"
                        },
                        new Cart()
                        {
                            Id = Guid.NewGuid(),
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
                            Id = "100020",
                            Name = "Costume Bundle",
                            Discount = 0.20M,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now + TimeSpan.FromDays(100)

                        },
                        new Bundle()
                        {
                            Id = "100021",
                            Name = "Costume Bundle with Makeup",
                            Discount = 0.20M,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now + TimeSpan.FromDays(100)

                        },
                        new Bundle()
                        {
                            Id = "100022",
                            Name = "Bookworm (Seagull-Worm) Bundle",
                            Discount = 0.20M,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now + TimeSpan.FromDays(100)

                        },
                        new Bundle()
                        {
                            Id = "100028",
                            Name = "Seagull Plushies",
                            Discount = 0.20M,
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
                            InventoryId = "000016"

                        },
                        new BundleItem()
                        {
                            BundleId = "100020",
                            InventoryId = "000017"

                        },
                        new BundleItem()
                        {
                            BundleId = "100021",
                            InventoryId = "000016"

                        },
                        new BundleItem()
                        {
                            BundleId = "100021",
                            InventoryId = "000017"

                        },
                        new BundleItem()
                        {
                            BundleId = "100021",
                            InventoryId = "000019"

                        },
                        new BundleItem()
                        {
                            BundleId = "100022",
                            InventoryId = "000010"

                        },
                        new BundleItem()
                        {
                            BundleId = "100022",
                            InventoryId = "000011"

                        },
                        new BundleItem()
                        {
                            BundleId = "100022",
                            InventoryId = "000012"

                        },
                        new BundleItem()
                        {
                            BundleId = "100028",
                            InventoryId = "000014"

                        },
                        new BundleItem()
                        {
                            BundleId = "100028",
                            InventoryId = "000015"

                        }
                    );

                }

                context.SaveChanges();

            }
        }
    }
    
}
