using AutoMapper;
using Ecommerce.Core.Featuers.ProductFeatuer.Query.Response;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ProductMapping.Query
{
    public class GetRecentSearchMapping : Profile
    {
        public GetRecentSearchMapping()
        {
            CreateMap<RecentSearch, GetRecentSearchResponse>();
        }
    }
}
