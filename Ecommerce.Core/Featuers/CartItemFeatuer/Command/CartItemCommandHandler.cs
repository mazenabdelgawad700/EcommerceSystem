using AutoMapper;
using Ecommerce.Core.Featuers.CartItemFeatuer.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CartItemFeatuer.Command
{
    public class CartItemCommandHandler : ReturnBaseHandler,
        IRequestHandler<AddCartItemCommand, ReturnBase<bool>>,
        IRequestHandler<DeleteCartItemCommand, ReturnBase<bool>>
    {
        private readonly ICartItemService _cartItemService;
        private readonly IMapper _mapper;

        public CartItemCommandHandler(ICartItemService cartItemService, IMapper mapper)
        {
            this._cartItemService = cartItemService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<bool>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            if (request.Quantity <= 0)
                return BadRequest<bool>("Invalid quantity");
            try
            {
                var mappedResult = _mapper.Map<CartItem>(request);
                var addResult = await _cartItemService.AddCartItemAsync(mappedResult);

                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteResult = await _cartItemService.DeleteCartItemAsync(request.Id);
                return deleteResult.Succeeded ? Success(true) : Failed<bool>(deleteResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
