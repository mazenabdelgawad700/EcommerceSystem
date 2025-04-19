namespace Ecommerce.Domain.Entities
{
    public class CartItem
    {
        public int CartId { get; set; }
        public ulong ProductId { get; set; }
        public byte Quantity { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
