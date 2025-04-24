namespace Ecommerce.Domain.Entities
{
    public class ProductSeller
    {
        public string SellerId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
