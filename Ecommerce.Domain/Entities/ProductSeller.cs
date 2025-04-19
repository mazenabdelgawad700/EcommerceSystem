namespace Ecommerce.Domain.Entities
{
    public class ProductSeller
    {
        public string UserId { get; set; }
        public ulong ProductId { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
