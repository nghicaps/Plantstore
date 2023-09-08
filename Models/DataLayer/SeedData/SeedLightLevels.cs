using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Plantstore.Models
{
    internal class SeedLightLevels : IEntityTypeConfiguration<LightLevel>
    {
        public void Configure(EntityTypeBuilder<LightLevel> entity)
        {
            entity.HasData(
                new LightLevel { LightLevelId = "l", Name = "Low" },
                new LightLevel { LightLevelId = "m", Name = "Medium" },
                new LightLevel { LightLevelId = "b", Name = "Bright" }
            );
        }
    }

}