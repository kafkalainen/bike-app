using Solita.Bike.Api.Models;

namespace Solita.Bike.Api
{
    using Microsoft.EntityFrameworkCore;
    
    public class BikeDatabaseContext : DbContext
    {
        public DbSet<Journey> Journeys => Set<Journey>();

        public BikeDatabaseContext(DbContextOptions<BikeDatabaseContext> options)
            : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}