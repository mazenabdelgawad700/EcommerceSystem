using Ecommerce.Core.Featuers.ProductFeatuer.Query.Response;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Query.Model
{
    public class SearchAboutProductQuery : IRequest<ReturnBase<IQueryable<SearchAboutProductResponse>>>
    {
        public string SearchQuery { get; set; }
    }
}
