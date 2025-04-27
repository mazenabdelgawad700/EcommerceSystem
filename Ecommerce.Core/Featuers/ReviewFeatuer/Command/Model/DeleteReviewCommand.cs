using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ReviewFeatuer.Command.Model
{
    public class DeleteReviewCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
    }
}
