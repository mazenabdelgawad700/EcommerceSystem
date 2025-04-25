using Ecommerce.Core.Featuers.CartFeatuer.Command.Model;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.CartFeatuer.Command.Handler
{
    public class CartCommandHandler : ReturnBaseHandler,
        IRequestHandler<DeleteCartCommand, ReturnBase<bool>>
    {

        private readonly ICartService _cartService;

        public CartCommandHandler(ICartService cartService)
        {
            this._cartService = cartService;
        }

        public async Task<ReturnBase<bool>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteResult = await _cartService.DeleteCartAsync(request.Id);
                if (!deleteResult.Succeeded)
                    return Failed<bool>(deleteResult.Message);
                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
