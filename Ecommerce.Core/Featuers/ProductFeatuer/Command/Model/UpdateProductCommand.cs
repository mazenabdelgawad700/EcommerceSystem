using Ecommerce.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Command.Model
{
    public class UpdateProductCommand : IRequest<ReturnBase<bool>>
    {
        public ulong Id { get; set; }
        public string Name { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string SellerId { get; set; } = null!;
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Files { get; set; }
        public IEnumerable<IFormFile> NewFiles { get; set; }
    }
}
