using AutoMapper;
using Ecommerce.Core.Featuers.ProductFeatuer.Query.Response;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ProductMapping.Query
{
    public class SearchAboutProductMapping : Profile
    {
        public SearchAboutProductMapping()
        {
            CreateMap<Product, SearchAboutProductResponse>();
        }
    }
}
