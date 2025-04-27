using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.BrandFeatuer.Command.Model
{
    public class DeleteBrandCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
    }
}
