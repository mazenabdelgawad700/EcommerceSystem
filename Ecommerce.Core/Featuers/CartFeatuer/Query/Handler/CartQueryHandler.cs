using AutoMapper;
using Ecommerce.Core.Featuers.CartFeatuer.Query.Model;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Ecommerce.Shared.SharedResponse;
using MediatR;

namespace Ecommerce.Core.Featuers.CartFeatuer.Query.Handler
{
    public class CartQueryHandler : ReturnBaseHandler,
        IRequestHandler<GetCartQuery, ReturnBase<GetCartResponse>>
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartQueryHandler(ICartService cartService, IMapper mapper)
        {
            this._cartService = cartService;
            this._mapper = mapper;
        }

        public async Task<ReturnBase<GetCartResponse>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cartResult = await _cartService.GetCartAsync(request.UserId);
                if (!cartResult.Succeeded)
                    return Failed<GetCartResponse>(cartResult.Message);

                var responseDto = _mapper.Map<GetCartResponse>(cartResult.Data);

                return Success(responseDto);
            }
            catch (Exception ex)
            {
                return Failed<GetCartResponse>(ex.InnerException.Message);
            }
        }
    }
}
