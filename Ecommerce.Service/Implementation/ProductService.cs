using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.Implementation
{
    internal class ProductService : ReturnBaseHandler, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IImageService _imageService;
        public ProductService(IProductRepository productRepository, IImageService imageService, IProductImageRepository productImageRepository)
        {
            this._productRepository = productRepository;
            _imageService = imageService;
            _productImageRepository = productImageRepository;
        }

        public async Task<ReturnBase<ulong>> AddProductAsync(Product product, string userRole, string userId)
        {
            try
            {
                if (userRole.Equals("Admin"))
                    product.SellerId = "ME";

                else if (userRole.Equals("Seller"))
                    product.SellerId = userId;

                var saveProduct = await _productRepository.AddAsync(product);

                if (!saveProduct.Succeeded)
                    return Failed<ulong>();

                return Success(product.Id, "Product Saved Successfully");
            }
            catch (Exception ex)
            {
                return Failed<ulong>(ex.Message);
            }
        }
        public async Task<ReturnBase<bool>> SaveProductImagesAsync(IEnumerable<IFormFile> files, ulong productId)
        {
            foreach (var file in files)
            {
                var saveResult = await _imageService.SaveAsync(file);
                if (saveResult.Succeeded)
                {
                    var saveToDbResult = await _productImageRepository.SaveProductImage(productId, saveResult.Data);
                    if (!saveToDbResult)
                    {
                        _imageService.Delete(saveResult.Data);
                        return Failed<bool>("Failed to save some image, please try again");
                    }
                }

            }
            return Success(true, "Images Saved Successfully");
        }
    }
}
