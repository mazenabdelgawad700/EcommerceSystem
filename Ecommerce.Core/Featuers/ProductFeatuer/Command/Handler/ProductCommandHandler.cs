﻿using AutoMapper;
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
        IRequestHandler<DeleteProductCommand, ReturnBase<bool>>,
        IRequestHandler<SaveRecentSearchResultCommand, ReturnBase<bool>>
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

                var saveToInventoryResult = await _productInventoryService.AddProductInventoryEntity(addProduct.Data, request.InventoryId);

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
                return Failed<bool>(ex.InnerException.Message);
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
                return Failed<bool>(ex.InnerException.Message);
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

                var DeleteProductFromInventoryAsyncResult = await _productInventoryService.DeleteProductFromInventoryAsync(request.Id);

                if (!DeleteProductFromInventoryAsyncResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return Failed<bool>(DeleteProductFromInventoryAsyncResult.Message);
                }

                await transaction.CommitAsync();
                return Success(true, updateProduct.Message);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Failed<bool>(ex.InnerException.Message);
            }
        }

        public async Task<ReturnBase<bool>> Handle(SaveRecentSearchResultCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedResult = _mapper.Map<RecentSearch>(request);

                var saveRecentSearchResult = await _productService.SaveRecentSearchResultAsync(mappedResult);

                if (!saveRecentSearchResult.Succeeded)
                    return Failed<bool>(saveRecentSearchResult.Message);

                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
    }
}
