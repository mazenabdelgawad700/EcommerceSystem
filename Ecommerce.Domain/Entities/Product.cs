using Ecommerce.Shared.Enums;

namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string SellerId { get; set; }
        public ProductEnum Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public Discount Discount { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductVariants> ProductVariants { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<PreferredProducts> PreferredProducts { get; set; }
        public ICollection<ProductInventory> ProductInventories { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ProductSeller ProductSeller { get; set; }
    }
}
