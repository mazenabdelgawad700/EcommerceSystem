using Ecommerce.Shared.Base;
using Ecommerce.Shared.Enums;
using MediatR;

namespace Ecommerce.Core.Featuers.ShippingAddressesFeatuer.Command.Model
{
    public class AddShippingAddressesCommand : IRequest<ReturnBase<bool>>
    {
        public int OrderId { get; set; }
        public int PostalCode { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public ShippingEnum Status { get; set; }
    }
}
