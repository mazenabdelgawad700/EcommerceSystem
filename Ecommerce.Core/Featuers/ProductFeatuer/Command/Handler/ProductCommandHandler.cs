using AutoMapper;
using Ecommerce.Core.Featuers.ProductFeatuer.Command.Model;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using MediatR;

namespace Ecommerce.Core.Featuers.ProductFeatuer.Command.Handler
{
    public class ProductCommandHandler : ReturnBaseHandler,
        IRequestHandler<AddProductCommand, ReturnBase<bool>>
    {

        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductCommandHandler(IProductService productService, IMapper mapper, IProductRepository productRepository)
        {
            this._productService = productService;
            this._mapper = mapper;
            this._productRepository = productRepository;
        }

        public async Task<ReturnBase<bool>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _productRepository.BeginTransactionAsync();
            try
            {
                if (string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.UserRole))
                    return BadRequest<bool>("User id and User Role are required");

                var mappedResult = _mapper.Map<Product>(request);
                var addProduct = await _productService.AddProductAsync(mappedResult, request.UserRole, request.UserId);


                if (!addProduct.Succeeded)
                    return Failed<bool>("Failed to add product, please try again");

                var saveImagesResult = await _productService.SaveProductImagesAsync(request.Files, addProduct.Data);
                if (!saveImagesResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return Failed<bool>("Failed To Save Product Images, pleast try again");
                }

                // Add to product inventory table

                await transaction.CommitAsync();
                return Success(true, addProduct.Message);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Failed<bool>(ex.Message);
            }
        }
    }
}
