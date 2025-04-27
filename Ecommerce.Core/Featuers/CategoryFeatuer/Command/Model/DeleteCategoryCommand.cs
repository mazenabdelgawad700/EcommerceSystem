using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CategoryFeatuer.Command.Model
{
    public class DeleteCategoryCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
    }
}
