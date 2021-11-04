using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Domain.Models;

namespace RecipeBook.Infrastructure.EntityFramework
{
    public static class PrepDb
    {
        public static async Task SeedDatabase(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetService<RecipeBookContext>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

            if (userManager != null)
            {
                await userManager.SeedDefaultUsers();
            }

            if (roleManager != null)
            {
                await roleManager.SeedUserRoles();
            }

            if (context != null)
            {
                context.SeedUnitsOfMeasurement();
                context.SeedRecipes();

                var recipes = context.Recipes.ToList();
            }
        }

        private static async Task SeedDefaultUsers(this UserManager<User> userManager)
        {
            var defaultEmail = "robinlanderloos@gmail.com";
            var defaultPassword = "Rec1peB00k";

            var defaultUser = new User()
            {
                Email = defaultEmail,
                UserName = defaultEmail,
                FirstName = "Robin",
                LastName = "Landerloos"
            };

            if (await userManager.FindByEmailAsync(defaultEmail) == null)
            {
                await userManager.CreateAsync(new User()
                {
                    Email = "robinlanderloos@gmail.com",
                    UserName = defaultEmail

                }, defaultPassword);

                await userManager.AddToRoleAsync(defaultUser, Authorization.DefaultRole.ToString());
            }
        }

        private static async Task SeedUserRoles(this RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(Authorization.Roles)))
            {
                if (await roleManager.FindByNameAsync(role.ToString()) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
            }
        }

        private static void SeedUnitsOfMeasurement(this RecipeBookContext context)
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

        private static void SeedRecipes(this RecipeBookContext context)
        {
            var pastaCarbonara = new Recipe()
            {
                Name = "Pasta Carbonara",
                Description = "A creamy dish from Italy",
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
