namespace Plantstore.Models
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> List { get; set; }
        public double Subtotal { get; set; }
        public RouteDictionary PlantGridRoute { get; set; }
    }
}
