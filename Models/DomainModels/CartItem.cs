using Newtonsoft.Json;

namespace Plantstore.Models
{
    public class CartItem
    {
        public PlantDTO Plant { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double Subtotal => Plant.Price * Quantity;
    }
}
