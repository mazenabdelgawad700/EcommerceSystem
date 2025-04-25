using AutoMapper;
using Ecommerce.Core.Featuers.CartItemFeatuer.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.CartMapping.Command
{
    public class AddCartItemMapping : Profile
    {
        public AddCartItemMapping()
        {
            CreateMap<AddCartItemCommand, CartItem>();
        }
    }
}
