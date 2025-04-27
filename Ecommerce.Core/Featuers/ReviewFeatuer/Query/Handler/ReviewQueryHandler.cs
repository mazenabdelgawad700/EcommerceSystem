using AutoMapper;
using Ecommerce.Core.Featuers.ReviewFeatuer.Query.Response;
using Ecommerce.Core.Wrappers;
using Ecommerce.Service.Abstraction;
using Ecommerce.Service.Abstraction.Model;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ReviewFeatuer.Query.Handler
{
    public class ReviewQueryHandler : ReturnBaseHandler,
        IRequestHandler<GetProductReviewsQuery, ReturnBase<PaginatedResult<GetProductRviewsResponse>>>
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewQueryHandler(IReviewService reviewService, IMapper mapper)
        {
            this._reviewService = reviewService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<PaginatedResult<GetProductRviewsResponse>>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getProductReviewResult = _reviewService.GetProductReviewsAsync(request.ProductId);
                var mappedResult = await _mapper.ProjectTo<GetProductRviewsResponse>(getProductReviewResult.Data).ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return getProductReviewResult.Succeeded ? Success(mappedResult) : Failed<PaginatedResult<GetProductRviewsResponse>>(getProductReviewResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<PaginatedResult<GetProductRviewsResponse>>(ex.InnerException.Message);
            }
        }
    }
}
