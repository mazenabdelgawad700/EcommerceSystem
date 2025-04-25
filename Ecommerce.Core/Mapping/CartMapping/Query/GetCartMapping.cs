using AutoMapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Shared.SharedResponse;

namespace Ecommerce.Core.Mapping.CartMapping.Query
{
    public class GetCartMapping : Profile
    {
        public GetCartMapping()
        {
            CreateMap<Cart, GetCartResponse>()
             .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src =>
                    src.Product.ProductImages.FirstOrDefault().ImageUrl))
                .ForMember(dest => dest.PricePerUnit, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
