namespace Ecommerce.Domain.Entities
{
    public class RecentSearch
    {
        public string UserId { get; set; } = null!;
        public string SearchQuery { get; set; } = null!;
        public ApplicationUser User { get; set; }
    }
}