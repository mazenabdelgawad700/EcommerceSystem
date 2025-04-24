namespace Ecommerce.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> OrderItems { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public Transaction Transaction { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ApplicationUser User { get; set; }  // Added navigation property back to user
    }
}
