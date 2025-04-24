using Ecommerce.Core.Featuers.ProductFeatuer.Query.Response;
using Ecommerce.Core.Wrappers;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Query.Model
{
    public class GetProductAsPaginatedListQuery : IRequest<ReturnBase<PaginatedResult<GetProductAsPaginatedListResponse>>>
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
