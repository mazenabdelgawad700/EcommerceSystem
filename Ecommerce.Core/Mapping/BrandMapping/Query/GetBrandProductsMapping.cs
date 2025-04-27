using AutoMapper;
using Ecommerce.Core.Featuers.BrandFeatuer.Query.Response;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.BrandMapping.Query
{
    public class GetBrandProductsMapping : Profile
    {
        public GetBrandProductsMapping()
        {
            CreateMap<Brand, GetBrandProductsResponse>();
            CreateMap<Product, GetBrandProductsResponse>();
        }
    }
}
