namespace Plantstore.Models
{
    public static class CartItemListExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO
            {
                PlantId = ci.Plant.PlantId,
                Quantity = ci.Quantity
            }).ToList();
    }
}
