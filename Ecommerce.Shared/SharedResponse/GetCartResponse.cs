namespace Ecommerce.Shared.SharedResponse
{
    public class GetCartResponse
    {
        public int CartId { get; set; }
        public IEnumerable<CartItemDto>? Items { get; set; }
    }
    public class CartItemDto
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public byte Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
