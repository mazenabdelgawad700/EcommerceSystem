using Ecommerce.Shared.Enums;

namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        //public ulong Id { get; set; }
        //public string Name { get; set; } = null!;
        //public string Description { get; set; } = null!;
        //public uint BrandId { get; set; }
        //public uint CategoryId { get; set; }
        //public decimal Price { get; set; }
        //public string SellerId { get; set; }
        //public ProductEnum Status { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        //public Inventory Inventory { get; set; }
        //public Category Category { get; set; }
        //public Brand Brand { get; set; }
        //public Discount Discount { get; set; }
        //public ICollection<Review> Reviews { get; set; }
        //public ICollection<ProductVariants> ProductVariants { get; set; }
        //public ICollection<ProductImage> ProductImages { get; set; }
        //public ICollection<PreferredProducts> PreferredProducts { get; set; }
        //public ICollection<ProductInventory> ProductInventories { get; set; }
        //public ICollection<CartItem> CartItems { get; set; }
        //public ProductSeller ProductSeller { get; set; }

        public ulong Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int BrandId { get; set; }  // Changed from uint to int to match Brand.Id
        public int CategoryId { get; set; }  // Changed from uint to int to match Category.Id
        public decimal Price { get; set; }
        public string SellerId { get; set; }
        public ProductEnum Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Removed direct Inventory property as it's handled through ProductInventory
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public Discount Discount { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductVariants> ProductVariants { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<PreferredProducts> PreferredProducts { get; set; }
        public ICollection<ProductInventory> ProductInventories { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }  // Added missing collection
        public ProductSeller ProductSeller { get; set; }


    }
}
