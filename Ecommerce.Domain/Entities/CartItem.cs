namespace Ecommerce.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public byte Quantity { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
