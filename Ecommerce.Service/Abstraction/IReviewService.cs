using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IReviewService
    {
        Task<ReturnBase<bool>> AddReviewAsync(Review review);
        Task<ReturnBase<bool>> DeleteReviewAsync(int reviewId);
    }
}
