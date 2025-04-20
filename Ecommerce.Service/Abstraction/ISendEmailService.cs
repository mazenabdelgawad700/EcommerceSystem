using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface ISendEmailService
    {
        Task<ReturnBase<bool>> SendEmailAsync(string email, string message, string subject, string contentType = "text/plain");
    }
}
