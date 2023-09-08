using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Plantstore.Models
{
    internal class SeedScientifics : IEntityTypeConfiguration<Scientific>
    {
        public void Configure(EntityTypeBuilder<Scientific> entity)
        {
            entity.HasData(
                new Scientific { PlantId = 1, ScientificNameId = 1 }
            );
        }
    }

}