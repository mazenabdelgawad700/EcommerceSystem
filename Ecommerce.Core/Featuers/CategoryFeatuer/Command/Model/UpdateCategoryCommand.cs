using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CategoryFeatuer.Command.Model
{
    public class UpdateCategoryCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
