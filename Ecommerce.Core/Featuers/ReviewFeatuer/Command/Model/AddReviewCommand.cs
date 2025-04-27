using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ReviewFeatuer.Command.Model
{
    public class AddReviewCommand : IRequest<ReturnBase<bool>>
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public byte Rating { get; set; }
    }
}
