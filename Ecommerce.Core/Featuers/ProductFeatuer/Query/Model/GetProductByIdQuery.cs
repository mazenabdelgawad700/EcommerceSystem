using Ecommerce.Core.Featuers.ProductFeatuer.Query.Response;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Query.Model
{
    public class GetProductByIdQuery : IRequest<ReturnBase<GetProductByIdResponse>>
    {
        public int Id { get; set; }
    }
}
