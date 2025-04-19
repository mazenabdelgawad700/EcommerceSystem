namespace Ecommerce.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public ulong ProductId { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
