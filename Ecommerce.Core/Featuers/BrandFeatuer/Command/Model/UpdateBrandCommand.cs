using Ecommerce.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Core.Featuers.BrandFeatuer.Command.Model
{
    public class UpdateBrandCommand : IRequest<ReturnBase<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Logo { get; set; }
        public string OldLogoUrl { get; set; }
    }
}
