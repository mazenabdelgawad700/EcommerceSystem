using AutoMapper;
using Ecommerce.Core.Featuers.ProductFeatuer.Query.Model;
using Ecommerce.Core.Featuers.ProductFeatuer.Query.Response;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Query.Handler
{
    public class ProductQueryHandler : ReturnBaseHandler, IRequestHandler<GetProductByIdQuery, ReturnBase<GetProductByIdResponse>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;


        public ProductQueryHandler(IProductService productService, IMapper mapper)
        {
            this._productService = productService;
            this._mapper = mapper;
        }


        public async Task<ReturnBase<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getProductResult = await _productService.GetProductByIdAsync(request.Id);

                if (!getProductResult.Succeeded)
                    return Failed<GetProductByIdResponse>(getProductResult.Message);

                var mappedResult = _mapper.Map<GetProductByIdResponse>(getProductResult.Data);

                return Success(mappedResult);
            }
            catch (Exception ex)
            {
                return Failed<GetProductByIdResponse>(ex.InnerException.Message);
            }
        }
    }
}
