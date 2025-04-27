using Ecommerce.Core.Featuers.CategoryFeatuer.Query.Response;
using Ecommerce.Core.Wrappers;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CategoryFeatuer.Query.Model
{
    public class GetCategoryProductsQuery : IRequest<ReturnBase<PaginatedResult<GetCategoryProductsResponse>>>
    {
        public int CategoryId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
