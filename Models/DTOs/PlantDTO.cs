namespace Plantstore.Models
{
    public class PlantDTO
    {
        public int PlantId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public Dictionary<int, string> ScientificNames { get; set; }

        public void Load(Plant plant)
        {
            PlantId = plant.PlantId;
            Title = plant.Title;
            Price = plant.Price;
            ScientificNames = new Dictionary<int, string>();
            foreach (Scientific s in plant.Scientifics)
            {
                ScientificNames.Add(s.ScientificName.ScientificNameId, s.ScientificName.FullName);
            }
        }
    }

}
