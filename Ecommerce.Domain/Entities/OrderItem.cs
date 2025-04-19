namespace Ecommerce.Domain.Entities
{
    public class OrderItem
    {
        public ulong OrderId { get; set; }
        public Order Order { get; set; }
        public ulong ProductId { get; set; }
        public Product Product { get; set; }
        public byte Quantity { get; set; }
        public decimal PiecePrice { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
