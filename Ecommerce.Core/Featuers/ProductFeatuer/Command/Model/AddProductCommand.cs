using Ecommerce.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Command.Model
{
    public class AddProductCommand : IRequest<ReturnBase<bool>>
    {
        public string Name { get; set; } = null!;
        public string? UserRole { get; set; } // Get from the token
        public string? UserId { get; set; } // Get from the token
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
