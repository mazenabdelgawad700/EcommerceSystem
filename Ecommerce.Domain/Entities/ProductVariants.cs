namespace Ecommerce.Domain.Entities
{
    public class ProductVariants
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Value { get; set; }
        public string VariantType { get; set; }
        public Product Product { get; set; }
    }
}
