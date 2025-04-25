using AutoMapper;
using Ecommerce.Core.Featuers.ProductInventoryFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductInventoryFeatuer.Command.Handler
{
    public class ProductInventoryHandler : ReturnBaseHandler,
        IRequestHandler<UpdateProductQuantityCommand, ReturnBase<bool>>
    {
        private readonly IProductInventoryService _productInventoryService;
        private readonly IMapper _mapper;

        public ProductInventoryHandler(IProductInventoryService productInventoryService, IMapper mapper)
        {
            this._productInventoryService = productInventoryService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<bool>> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            if (request.Quantity <= 0)
                return BadRequest<bool>("Invalid Quantity");
            try
            {
                var mappedResult = _mapper.Map<ProductInventory>(request);
                var updateResult = await _productInventoryService.UpdateProductQuantityAsync(mappedResult);

                return updateResult.Succeeded ? Success(true) : Failed<bool>(updateResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
