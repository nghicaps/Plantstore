using Newtonsoft.Json;

namespace Plantstore.Models
{
    public class PlantsGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string ScientificName { get; set; } = DefaultFilter;
        public string LightLevel { get; set; } = DefaultFilter;
        public string Price { get; set; } = DefaultFilter;
    }
}
