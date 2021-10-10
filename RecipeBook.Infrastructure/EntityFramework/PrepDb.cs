using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Domain.Models;

namespace RecipeBook.Infrastructure.EntityFramework
{
    public static class PrepDb
    {
        public static void SeedDatabase(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetService<RecipeBookContext>();

            SeedUnitsOfMeasurement(context);
            SeedRecipes(context);

            var recipes = context.Recipes.ToList();
        }

        private static void SeedUnitsOfMeasurement(RecipeBookContext context)
        {
            var units = new List<UnitOfMeasurement>()
            {
                new()
                {
                    Name = "gr",
                    Description = "Grams",
                    CreatedAt= DateTime.Now
                },
                new()
                {
                    Name = "kg",
                    Description = "Kilograms",
                    CreatedAt= DateTime.Now
                },
                new()
                {
                    Name = "l",
                    Description = "Liters",
                    CreatedAt= DateTime.Now
                },
                new()
                {
                    Name = "cl",
                    Description = "Centiliters",
                    CreatedAt= DateTime.Now
                },
            };

            SeedSet(context, units);
        }

        private static void SeedRecipes(RecipeBookContext context)
        {
            var pastaCarbonara = new Recipe()
            {
                Name = "Pasta Carbonara",
                Description = "A Creamy dish from Italy",
                CreatedAt = DateTime.Now,
                Servings = 4,
                IngredientLines = new List<IngredientLine>()
                {
                    new()
                    {
                        CreatedAt= DateTime.Now,
                        Ingredient = "Pasta",
                        UnitOfMeasurementId = 1,
                        IngredientAmount = 400,
                    },
                    new()
                    {
                        CreatedAt= DateTime.Now,
                        Ingredient = "Cream",
                        UnitOfMeasurementId = 4,
                        IngredientAmount = 500,
                    },
                    new()
                    {
                        CreatedAt= DateTime.Now,
                        Ingredient = "Bacon",
                        UnitOfMeasurementId = 1,
                        IngredientAmount = 300,
                    }
                }
            };

            SeedSet(context, new List<Recipe>() { pastaCarbonara });
        }

        private static void SeedSet<T>(RecipeBookContext context, ICollection<T> entities)
            where T : class
        {
            if (!entities.Any())
            {
                throw new ArgumentException(nameof(entities));
            }

            if (context == null)
            {
                throw new ArgumentException(nameof(context));
            }

            if (!context.Set<T>().Any())
            {
                context.Set<T>().AddRange(entities);
            }

            context.SaveChanges();
        }
    }
}
