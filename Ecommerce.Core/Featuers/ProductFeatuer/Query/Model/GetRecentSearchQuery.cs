using Ecommerce.Core.Featuers.ProductFeatuer.Query.Response;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Query.Model
{
    public class GetRecentSearchQuery : IRequest<ReturnBase<IQueryable<GetRecentSearchResponse>>>
    {
        public string UserId { get; set; }
    }
}
