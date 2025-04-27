using AutoMapper;
using Ecommerce.Core.Featuers.ReviewFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ReviewFeatuer.Command.Handler
{
    public class ReviewCommandHandler : ReturnBaseHandler,
        IRequestHandler<AddReviewCommand, ReturnBase<bool>>,
        IRequestHandler<DeleteReviewCommand, ReturnBase<bool>>
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewCommandHandler(IReviewService reviewService, IMapper mapper)
        {
            this._reviewService = reviewService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<bool>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Rating < 0 || request.Rating > 5)
                    return BadRequest<bool>("Invlaid rate Value");

                var mappedResult = _mapper.Map<Review>(request);
                var addResult = await _reviewService.AddReviewAsync(mappedResult);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }

        public async Task<ReturnBase<bool>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteResult = await _reviewService.DeleteReviewAsync(request.Id);
                return deleteResult.Succeeded ? Success(true) : Failed<bool>(deleteResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
