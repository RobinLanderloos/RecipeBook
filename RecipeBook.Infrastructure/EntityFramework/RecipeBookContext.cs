using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RecipeBook.Domain.Models;
using RecipeBook.Domain.Models.Base;

namespace RecipeBook.Infrastructure.EntityFramework
{
    public class RecipeBookContext : IdentityDbContext<User>
    {
        public RecipeBookContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<IngredientLine> IngredientLines { get; set; }
        public DbSet<UnitOfMeasurement> UnitsOfMeasurement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.IngredientLines)
                .WithOne(x => x.Recipe);

            modelBuilder.Entity<IngredientLine>()
                .HasOne(x => x.Recipe)
                .WithMany(x => x.IngredientLines);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            var addedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditEntity
                && e.State == EntityState.Added
                || e.State == EntityState.Modified);

            foreach (var addedEntry in addedEntries)
            {
                var timestamp = DateTime.Now;
                var entity = (AuditEntity)addedEntry.Entity;
                entity.UpdatedAt = timestamp;

                if (addedEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = timestamp;
                }
            }
        }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RecipeBookContext>
    {
        public RecipeBookContext CreateDbContext(string[] args)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../MyCookingMaster.API/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<RecipeBookContext>();
            var connectionString = @"Data Source=DESKTOP-ROBIN\DESKTOPROBIN;Initial Catalog=RecipeBook;Integrated Security=True";
            builder.UseSqlServer(connectionString);
            return new RecipeBookContext(builder.Options);
        }
    }
}
