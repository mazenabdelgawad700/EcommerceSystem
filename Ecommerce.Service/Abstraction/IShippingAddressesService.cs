using Ecommerce.Domain.Entities;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Abstraction
{
    public interface IShippingAddressesService
    {
        Task<ReturnBase<bool>> AddShippingAddressAsync(ShippingAddress shippingAddress);
    }
}
