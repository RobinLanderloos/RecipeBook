using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.API.ResponseHandlers;
using RecipeBook.API.Services;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.Models.Dtos.IngredientLine;
using RecipeBook.Infrastructure.Models.Dtos.Recipe;
using System.Text;

namespace RecipeBook.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddResponseHandlers(this IServiceCollection services)
        {
            services.AddScoped<IResponseHandler<RecipeDto, RecipeCreateDto, Recipe, GetSingleRecipeDto>, RecipeResponseHandler>();
            services.AddScoped<IResponseHandler<IngredientLineDto, IngredientLineCreateDto, IngredientLine, GetSingleIngredientDto>, IngredientLineResponseHandler>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }

        public static void AddDbContexts<TDBContext, TIdentityContext, TUser>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
            where TDBContext : DbContext
            where TIdentityContext : DbContext
            where TUser : class
        {
            services.AddDbContext<TDBContext>(optionsAction);
            services.AddIdentityCore<TUser>(options => { });
            new IdentityBuilder(typeof(TUser), typeof(IdentityRole), services)
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddSignInManager<SignInManager<TUser>>()
                .AddEntityFrameworkStores<TDBContext>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });
        }

        public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });
        }
    }
}
