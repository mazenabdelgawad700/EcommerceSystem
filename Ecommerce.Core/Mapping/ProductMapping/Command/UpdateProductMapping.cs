using AutoMapper;
using Ecommerce.Core.Featuers.ProductFeatuer.Command.Model;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ProductMapping.Command
{
    public class UpdateProductMapping : Profile
    {
        public UpdateProductMapping()
        {
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(src => src.SellerId, dest => dest.MapFrom(src => src.SellerId))
                .ForMember(src => src.Price, dest => dest.MapFrom(src => src.Price));

        }
    }
}
