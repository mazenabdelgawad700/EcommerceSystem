using AutoMapper;
using Ecommerce.Core.Featuers.ShippingAddressesFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ShippingAddressesMapping.Command
{
    public class AddShippingAddressesMapping : Profile
    {
        public AddShippingAddressesMapping()
        {
            CreateMap<AddShippingAddressesCommand, ShippingAddress>();
        }
    }
}
