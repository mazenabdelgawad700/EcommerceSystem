using AutoMapper;
using Ecommerce.Core.Featuers.CategoryFeatuer.Query.Response;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.CategoryMapping.Query
{
    public class GetCategoryProductsMapping : Profile
    {
        public GetCategoryProductsMapping()
        {
            CreateMap<Category, GetCategoryProductsResponse>();
            CreateMap<Product, GetCategoryProductsResponse>();
        }
    }
}
