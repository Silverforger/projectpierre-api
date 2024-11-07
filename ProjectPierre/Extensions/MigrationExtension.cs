using Microsoft.EntityFrameworkCore;
using ProjectPierre.Data;
using ProjectPierre.Models;

namespace ProjectPierre.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using DataContext dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            dbContext.Database.Migrate();

            // Data seeding - categories and products
            if (!dbContext.Categories.Any() && !dbContext.Products.Any())
            {
                SeedData(dbContext);
            }
            else
            {
                System.Console.WriteLine("No seeding needed!");
            }
        }

        public static void SeedData(DataContext context)
        {
            System.Console.WriteLine("Seeding in progress...");

            var beerCat = new Category
            {
                Name = "Beer",
                Aisles = new List<Aisle>()
            };

            var fruitCat = new Category
            {
                Name = "Fruit Products",
                Aisles = new List<Aisle>()
            };

            var miscCat = new Category
            {
                Name = "Misc",
                Aisles = new List<Aisle>()
            };

            context.Categories.AddRange(new List<Category>
            {
                beerCat,
                fruitCat,
                miscCat
            });
            context.SaveChanges();

            var horoyoi = new Product
            {
                Label = "Horoyoi",
                Description = "Horoyoi beer",
                Price = 50000
            };

            var blackLabel = new Product
            {
                Label = "Black Label",
                Description = "Black Label beer",
                Price = 23000
            };

            var banana = new Product
            {
                Label = "Banana",
                Description = "Banana fruit",
                Price = 50
            };

            var khakis = new Product
            {
                Label = "Khaki shorts",
                Description = "Just some bing chilling khakis",
                Price = 50123
            };

            var bananaShake = new Product
            {
                Label = "Banana beer",
                Description = "huh",
                Price = 12
            }; 

            context.Products.AddRange(new List<Product>
            {
                horoyoi,
                blackLabel,
                banana,
                khakis,
                bananaShake
            });
            context.SaveChanges();

            var dummyAisles = new List<Aisle>
            {
                new Aisle
                {
                    ProductId = horoyoi.Id,
                    CategoryId = beerCat.Id,
                },
                new Aisle
                {
                    ProductId = blackLabel.Id,
                    CategoryId = beerCat.Id,
                },
                new Aisle
                {
                    ProductId = banana.Id,
                    CategoryId = fruitCat.Id,
                },
                new Aisle
                {
                    ProductId = khakis.Id,
                    CategoryId = miscCat.Id,
                },
                new Aisle
                {
                    ProductId = bananaShake.Id,
                    CategoryId = fruitCat.Id,
                },
                new Aisle
                {
                    ProductId = bananaShake.Id,
                    CategoryId = beerCat.Id,
                }
            };

            context.Aisles.AddRange(dummyAisles);
            context.SaveChanges();
        }
    }
}
