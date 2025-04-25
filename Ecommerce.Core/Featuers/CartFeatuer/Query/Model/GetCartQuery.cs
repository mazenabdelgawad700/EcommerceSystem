using Ecommerce.Shared.Base;
using Ecommerce.Shared.SharedResponse;
using MediatR;

namespace Ecommerce.Core.Featuers.CartFeatuer.Query.Model
{
    public class GetCartQuery : IRequest<ReturnBase<GetCartResponse>>
    {
        public string UserId { get; set; }
    }
}
