namespace Ecommerce.Domain.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public ulong ProductId { get; set; }
        public string? ImageUrl { get; set; }
        public Product Product { get; set; }
    }
}
