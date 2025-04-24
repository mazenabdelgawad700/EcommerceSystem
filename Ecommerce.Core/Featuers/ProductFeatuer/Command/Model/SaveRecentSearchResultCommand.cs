using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Command.Model
{
    public class SaveRecentSearchResultCommand : IRequest<ReturnBase<bool>>
    {
        public string UserId { get; set; }
        public string SearchQuery { get; set; }
    }
}
