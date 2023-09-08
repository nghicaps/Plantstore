namespace Plantstore.Models
{
    public class Scientific
    {
        public int PlantId { get; set; }
        public int ScientificNameId { get; set; }

        public ScientificName ScientificName { get; set; }
        public Plant Plant { get; set; }
    }
}
