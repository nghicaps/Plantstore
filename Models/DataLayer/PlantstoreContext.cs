using Microsoft.EntityFrameworkCore;

namespace Plantstore.Models
{
    public class PlantstoreContext : DbContext
    {
        public PlantstoreContext(DbContextOptions<PlantstoreContext> options)
            : base(options)
        { }

        public DbSet<ScientificName> ScientificNames { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Scientific> Scientifics { get; set; }
        public DbSet<LightLevel> LightLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scientific>().HasKey(ba => new { ba.PlantId, ba.ScientificNameId });

            modelBuilder.Entity<Scientific>().HasOne(ba => ba.Plant)
                .WithMany(b => b.Scientifics)
                .HasForeignKey(ba => ba.PlantId);
            modelBuilder.Entity<Scientific>().HasOne(ba => ba.ScientificName)
                .WithMany(a => a.Scientifics)
                .HasForeignKey(ba => ba.ScientificNameId);

            modelBuilder.Entity<Plant>().HasOne(b => b.LightLevel)
                .WithMany(g => g.Plants)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new SeedLightLevels());
            modelBuilder.ApplyConfiguration(new SeedPlants());
            modelBuilder.ApplyConfiguration(new SeedScientificNames());
            modelBuilder.ApplyConfiguration(new SeedScientifics());
        }
    }
}
