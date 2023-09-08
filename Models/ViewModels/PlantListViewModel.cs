namespace Plantstore.Models
{
    public class PlantListViewModel
    {
        public IEnumerable<Plant> Plants { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<ScientificName> ScientificNames { get; set; }
        public IEnumerable<LightLevel> LightLevels { get; set; }
        public Dictionary<string, string> Prices =>
            new Dictionary<string, string> {
                { "under7", "Under $7" },
                { "7to14", "$7 to $14" },
                { "over14", "Over $14" }
            };
        public int[] PageSizes => new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    }
}
