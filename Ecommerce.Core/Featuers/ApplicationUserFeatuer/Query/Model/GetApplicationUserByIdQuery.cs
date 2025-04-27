using Ecommerce.Core.Featuers.ApplicationUserFeatuer.Query.Response;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ApplicationUserFeatuer.Query.Model
{
    public class GetApplicationUserByIdQuery : IRequest<ReturnBase<GetApplicationUserByIdResponse>>
    {
        public string Id { get; set; }
    }
}
