using AutoMapper;
using Ecommerce.Core.Featuers.ShippingAddressesFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ShippingAddressesFeatuer.Command.Handler
{
    public class ShippingAddressesCommandHandler : ReturnBaseHandler,
        IRequestHandler<AddShippingAddressesCommand, ReturnBase<bool>>
    {
        private readonly IShippingAddressesService _shippingAddressesService;
        private readonly IMapper _mapper;

        public ShippingAddressesCommandHandler(IShippingAddressesService shippingAddressesService, IMapper mapper)
        {
            this._shippingAddressesService = shippingAddressesService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<bool>> Handle(AddShippingAddressesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedResult = _mapper.Map<ShippingAddress>(request);
                var addResult = await _shippingAddressesService.AddShippingAddressAsync(mappedResult);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
