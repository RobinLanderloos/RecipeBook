using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.API.Extensions
{
    public static class ServiceCollectionExtensions 
    {
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


    }
}
