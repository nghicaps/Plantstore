using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Plantstore.Models
{
    internal class SeedScientificNames : IEntityTypeConfiguration<ScientificName>
    {
        public void Configure(EntityTypeBuilder<ScientificName> entity)
        {
            entity.HasData(
                new ScientificName { ScientificNameId = 1, Genus = "Monstera", Species = "deliciosa" }
            );
        }
    }

}