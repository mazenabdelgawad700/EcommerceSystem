namespace Ecommerce.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime SessionId { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
