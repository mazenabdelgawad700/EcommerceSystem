using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Entities
{
    public class ApplicationUser : IdentityUser<string>
    {
        //public string Role { get; set; }
        //public Cart Cart { get; set; }
        //public ICollection<Order> Orders { get; set; }
        //public ICollection<RecentSearch> RecentSearchs { get; set; }
        //public ICollection<Review> Reviews { get; set; }
        //public ICollection<ProductSeller> ProductSeller { get; set; }
        //public ShippingAddress ShippingAddress { get; set; }

        public string Role { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<RecentSearch> RecentSearches { get; set; }  // Fixed spelling
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductSeller> ProductSellers { get; set; }  // Pluralized for consistency
        public ICollection<PreferredProducts> PreferredProducts { get; set; }  // Added missing collection
        public ICollection<RefreshToken> RefreshTokens { get; set; }  // Added missing collection
        public ShippingAddress ShippingAddress { get; set; }
    }
}
