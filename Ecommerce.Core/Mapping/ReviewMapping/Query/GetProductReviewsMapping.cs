using AutoMapper;
using Ecommerce.Core.Featuers.ReviewFeatuer.Query.Response;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Mapping.ReviewMapping.Query
{
    public class GetProductReviewsMapping : Profile
    {
        public GetProductReviewsMapping()
        {
            CreateMap<Review, GetProductRviewsResponse>();
        }
    }
}
