namespace Ecommerce.Domain.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string PaymentMethodName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Order> Orders { get; set; }
    }
}
