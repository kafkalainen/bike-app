using Microsoft.EntityFrameworkCore;
using Solita.Bike.Models;

namespace Solita.Bike.Database
{
    public class BikeDbContext : DbContext
    {
        public DbSet<Station> Stations => Set<Station>();
        public DbSet<Journey> Journeys => Set<Journey>();
        public DbSet<DataImport> DataImports => Set<DataImport>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                Environment.GetEnvironmentVariable("ConnectionStrings__BikeConnection"),
                    new MySqlServerVersion(new Version(8, 0, 32)));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Journey>()
                .HasOne(j => j.DepartureStation)
                .WithMany(s => s.DepartureJourneys)
                .HasPrincipalKey(s => s.Id)
                .HasForeignKey(o => o.DepartureStationId);
            
            builder.Entity<Journey>()
                .HasOne(j => j.ReturnStation)
                .WithMany(s => s.ReturnJourneys)
                .HasPrincipalKey(s => s.Id)
                .HasForeignKey(j => j.ReturnStationId);
            
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}