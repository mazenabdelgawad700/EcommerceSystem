using Ecommerce.Core.Featuers.BrandFeatuer.Query.Response;
using Ecommerce.Core.Wrappers;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.BrandFeatuer.Query.Model
{
    public class GetBrandProductsQuery : IRequest<ReturnBase<PaginatedResult<GetBrandProductsResponse>>>
    {
        public int BrandId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
