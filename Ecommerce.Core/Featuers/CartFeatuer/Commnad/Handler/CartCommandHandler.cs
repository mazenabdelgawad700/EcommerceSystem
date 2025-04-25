//using AutoMapper;
//using Ecommerce.Core.Featuers.CartFeatuer.Commnad.Model;
//using Ecommerce.Domain.Entities;
//using Ecommerce.Service.Abstraction;
//using Ecommerce.Shared.Base;
//using MediatR;

//namespace Ecommerce.Core.Featuers.CartFeatuer.Commnad.Handler
//{
//    public class CartCommandHandler : ReturnBaseHandler,
//        IRequestHandler<CreateCartCommand, ReturnBase<bool>>
//    {
//        private readonly ICartService _cartService;
//        private readonly IMapper _mapper;

//        public CartCommandHandler(ICartService cartService, IMapper mapper)
//        {
//            this._cartService = cartService;
//            this._mapper = mapper;
//        }

//        public async Task<ReturnBase<bool>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                var mappedResult = _mapper.Map<Cart>(request);

//                var createCartResult = await _cartService.CreateCartAsync(mappedResult);

//                if (!createCartResult.Succeeded)
//                    return Failed<bool>(createCartResult.Message);

//                return Success(true);
//            }
//            catch (Exception ex)
//            {
//                return Failed<bool>(ex.InnerException.Message);
//            }
//        }
//    }
//}
