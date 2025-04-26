using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class ShippingAddressesService : ReturnBaseHandler, IShippingAddressesService
    {
        private readonly IShippingAddressesRepository _shippingAddressesRepository;

        public ShippingAddressesService(IShippingAddressesRepository shippingAddressesRepository)
        {
            this._shippingAddressesRepository = shippingAddressesRepository;
        }

        public async Task<ReturnBase<bool>> AddShippingAddressAsync(ShippingAddress shippingAddress)
        {
            try
            {
                var addResult = await _shippingAddressesRepository.AddAsync(shippingAddress);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
