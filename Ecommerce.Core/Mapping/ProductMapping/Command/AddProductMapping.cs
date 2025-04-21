using AutoMapper;
using Ecommerce.Core.Featuers.ProductFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ProductMapping.Command
{
    public class AddProductMapping : Profile
    {
        public AddProductMapping()
        {
            CreateMap<AddProductCommand, Product>()
                .ForMember(src => src.SellerId, dest => dest.Ignore())
                .ForMember(src => src.Price, dest => dest.MapFrom(src => src.Price));

        }
    }
}
