using Ecommerce.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Core.Featuers.BrandFeatuer.Command.Model
{
    public class AddBrandCommand : IRequest<ReturnBase<bool>>
    {
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
    }
}
