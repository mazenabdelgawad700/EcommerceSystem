using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Entities
{
    public class ApplicationUser : IdentityUser<string>
    {
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<RecentSearch> RecentSearches { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductSeller> ProductSellers { get; set; }
        public ICollection<PreferredProducts> PreferredProducts { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
    }
}
