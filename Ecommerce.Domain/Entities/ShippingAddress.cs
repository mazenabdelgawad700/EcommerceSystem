using Ecommerce.Shared.Enums;

namespace Ecommerce.Domain.Entities
{
    public class ShippingAddress
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; }
        public ulong OrderId { get; set; }
        public Order Order { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public ushort PostalCode { get; set; }
        public string Country { get; set; } = null!;
        public ShippingEnum Status { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
    }
}
