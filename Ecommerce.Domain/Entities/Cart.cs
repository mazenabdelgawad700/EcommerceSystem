namespace Ecommerce.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public string UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
