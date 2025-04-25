namespace Ecommerce.Shared.SharedResponse
{
    public class GetCartResponse
    {
        public IEnumerable<CartItemDto>? Items;
    }
    public class CartItemDto
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public byte Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
