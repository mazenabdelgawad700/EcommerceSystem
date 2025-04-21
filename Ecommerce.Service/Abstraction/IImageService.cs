using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Abstraction
{
    public interface IImageService
    {
        Task<ReturnBase<string>> SaveAsync(IFormFile file);
        ReturnBase<bool> Delete(string imgName);
    }
}
