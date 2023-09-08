using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Plantstore.Models
{
    internal class SeedPlants : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> entity)
        {
            entity.HasData(
                new Plant { PlantId = 1, Title = "Swiss Cheese Plant", LightLevelId = "b", Price = 5.00 }
            );
        }
    }

}