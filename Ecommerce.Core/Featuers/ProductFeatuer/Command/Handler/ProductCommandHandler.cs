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
        IRequestHandler<AddProductCommand, ReturnBase<bool>>,
        IRequestHandler<UpdateProductCommand, ReturnBase<bool>>,
        IRequestHandler<DeleteProductCommand, ReturnBase<bool>>
    {

        private readonly IProductService _productService;
        private readonly IProductInventoryService _productInventoryService;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductCommandHandler(IProductService productService, IMapper mapper, IProductRepository productRepository, IProductInventoryService productInventoryService)
        {
            this._productService = productService;
            this._mapper = mapper;
            this._productRepository = productRepository;
            _productInventoryService = productInventoryService;
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

                var saveToInventoryResult = await _productInventoryService.AddProductInventoryEntity(addProduct.Data, 1);

                if (!saveToInventoryResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return Failed<bool>(saveToInventoryResult.Message);
                }


                await transaction.CommitAsync();
                return Success(true, addProduct.Message);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Failed<bool>(ex.Message);
            }
        }

        public async Task<ReturnBase<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _productRepository.BeginTransactionAsync();
            try
            {
                var mappedResult = _mapper.Map<Product>(request);
                var updateProduct = await _productService.UpdateProductAsync(mappedResult);


                if (!updateProduct.Succeeded)
                    return Failed<bool>("Failed to Update product, please try again");

                if (request.Files is not null && request.NewFiles is not null)
                {
                    var saveImagesResult = await _productService.UpdateProductImagesAsync(request.NewFiles, request.Files, updateProduct.Data);
                    if (!saveImagesResult.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return Failed<bool>(saveImagesResult.Message);
                    }
                }

                await transaction.CommitAsync();
                return Success(true, updateProduct.Message);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Failed<bool>(ex.Message);
            }
        }

        public async Task<ReturnBase<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _productRepository.BeginTransactionAsync();
            try
            {
                var mappedResult = _mapper.Map<Product>(request);
                var updateProduct = await _productService.DeleteProductAsync(mappedResult);


                if (!updateProduct.Succeeded)
                    return Failed<bool>("Failed to Update product, please try again");

                var DeleteImagesResult = await _productService.DeleteProductImagesAsync(request.Id);

                if (!DeleteImagesResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return Failed<bool>("Failed to delete product because it failed to delete it's images");
                }

                var deleteProductFromInventoryResult = await _productInventoryService.DeleteProductFromInventory(request.Id);

                if (!deleteProductFromInventoryResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return Failed<bool>(deleteProductFromInventoryResult.Message);
                }

                await transaction.CommitAsync();
                return Success(true, updateProduct.Message);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Failed<bool>(ex.Message);
            }
        }
    }
}
