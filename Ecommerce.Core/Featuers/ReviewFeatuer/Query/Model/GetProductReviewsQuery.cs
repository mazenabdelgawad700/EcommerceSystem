using Ecommerce.Core.Featuers.ReviewFeatuer.Query.Response;
using Ecommerce.Core.Wrappers;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Service.Abstraction.Model
{
    public class GetProductReviewsQuery : IRequest<ReturnBase<PaginatedResult<GetProductRviewsResponse>>>
    {
        public int ProductId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
