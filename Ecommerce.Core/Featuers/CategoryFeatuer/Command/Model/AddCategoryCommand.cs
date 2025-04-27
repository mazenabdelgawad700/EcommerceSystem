using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CategoryFeatuer.Command.Model
{
    public class AddCategoryCommand : IRequest<ReturnBase<bool>>
    {
        public string Name { get; set; }
    }
}
