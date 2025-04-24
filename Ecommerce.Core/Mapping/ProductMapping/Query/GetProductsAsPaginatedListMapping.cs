using AutoMapper;
using Ecommerce.Core.Featuers.ProductFeatuer.Query.Response;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ProductMapping.Query
{
    public class GetProductsAsPaginatedListMapping : Profile
    {
        public GetProductsAsPaginatedListMapping()
        {
            CreateMap<Product, GetProductAsPaginatedListResponse>()
                .ForMember(dest => dest.ProductImagesUrls,
                    opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageUrl).ToList()));
        }
    }
}
