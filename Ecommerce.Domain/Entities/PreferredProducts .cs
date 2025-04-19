namespace Ecommerce.Domain.Entities
{
    public class PreferredProducts
    {
        public string UserId { get; set; }
        public ulong ProductId { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
