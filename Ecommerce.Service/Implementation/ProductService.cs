using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Service.Implementation
{
    internal class ProductService : ReturnBaseHandler, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IImageService _imageService;
        private readonly IRecentSearchRepository _recentSearchRepository;
        public ProductService(IProductRepository productRepository, IImageService imageService, IProductImageRepository productImageRepository, IRecentSearchRepository recentSearchRepository)
        {
            this._productRepository = productRepository;
            _imageService = imageService;
            _productImageRepository = productImageRepository;
            _recentSearchRepository = recentSearchRepository;
        }

        public async Task<ReturnBase<int>> AddProductAsync(Product product, string userRole, string userId)
        {
            try
            {
                if (userRole.Equals("Admin"))
                    product.SellerId = "ME";

                else if (userRole.Equals("Seller"))
                    product.SellerId = userId;

                var saveProduct = await _productRepository.AddAsync(product);

                if (!saveProduct.Succeeded)
                    return Failed<int>();

                return Success(product.Id, "Product Saved Successfully");
            }
            catch (Exception ex)
            {
                return Failed<int>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> DeleteProductAsync(Product product)
        {
            try
            {
                var deleteResult = await _productRepository.DeleteProductAsync(product);
                if (!deleteResult.Succeeded)
                    return Failed<bool>(deleteResult.Message);

                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> SaveProductImagesAsync(IEnumerable<IFormFile> files, int productId)
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
        public async Task<ReturnBase<int>> UpdateProductAsync(Product product)
        {
            try
            {
                var updateProduct = await _productRepository.UpdateAsync(product);

                if (!updateProduct.Succeeded)
                    return Failed<int>();

                return Success(product.Id, "Product Updated Successfully");
            }
            catch (Exception ex)
            {
                return Failed<int>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> UpdateProductImagesAsync(IEnumerable<IFormFile> files, IEnumerable<string> oldFiles, int productId)
        {
            foreach (var file in oldFiles)
            {
                var deleteFromDbResult = await _productImageRepository.DeleteProductImage(file, productId);
                if (!deleteFromDbResult)
                {
                    return Failed<bool>("Can not delete old images");
                }

                var deleteResult = _imageService.Delete(file);
                if (!deleteResult.Succeeded)
                    return Failed<bool>("Can not delete old images");
            }

            var saveNewImagesResult = await SaveProductImagesAsync(files, productId);

            if (!saveNewImagesResult.Succeeded)
                return Failed<bool>("Can not save new images, please try again");

            return Success(true, "Images Saved Successfully");
        }
        public async Task<ReturnBase<List<ProductImage>>> GetProductImagesForDelete(int productId)
        {
            try
            {
                var imagesResult = await _productImageRepository.GetAllImagesForProduct(productId);

                if (imagesResult.Succeeded)
                    return Success(imagesResult.Data);

                return Failed<List<ProductImage>>(imagesResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<List<ProductImage>>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> DeleteProductImagesAsync(int productId)
        {
            var getImagesResult = await GetProductImagesForDelete(productId);

            if (!getImagesResult.Succeeded)
                return Failed<bool>(getImagesResult.Message);

            foreach (var image in getImagesResult.Data)
            {
                var deleteFromDBResult = await _productImageRepository.DeleteProductImage(image.ImageUrl, image.ProductId);

                if (!deleteFromDBResult)
                    return Failed<bool>("Can not delete images");

                var deleteResult = _imageService.Delete(image.ImageUrl);
                if (!deleteResult.Succeeded)
                    return Failed<bool>("Can not delete images");
            }

            if (getImagesResult.Data.Count > 0)
                return Success(true, "Images deleted successfully");

            return Success(true);
        }
        public async Task<ReturnBase<Product>> GetProductByIdAsync(int productId)
        {
            try
            {
                var productResult = await _productRepository.GetByIdAsync(productId);

                if (!productResult.Succeeded)
                    return Failed<Product>(productResult.Message);

                return Success(productResult.Data);
            }
            catch (Exception ex)
            {
                return Failed<Product>(ex.InnerException.Message);
            }
        }
        public ReturnBase<IQueryable<Product>> GetProductsAsPaginated(int? categoryId = null, int? brandId = null)
        {
            try
            {
                var query = _productRepository.GetTableNoTracking().Data.Include(p => p.ProductImages).AsQueryable();
                if (categoryId is not null && brandId is not null)
                {
                    query = query.Where(x => x.CategoryId == categoryId.Value && x.BrandId == brandId);
                }
                else if (categoryId is not null && brandId is null)
                {
                    query = query.Where(x => x.CategoryId == categoryId.Value);
                }
                else if (categoryId is null && brandId is not null)
                {
                    query = query.Where(x => x.BrandId == brandId.Value);
                }
                return Success(query);
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<Product>>(ex.InnerException.Message);
            }
        }
        public ReturnBase<IQueryable<Product>> SearchAboutProduct(string? searchQuery = null)
        {
            try
            {
                var query = _productRepository.GetTableNoTracking().Data.Include(p => p.ProductImages).AsQueryable();
                if (searchQuery is not null)
                {
                    query = query.Where(x => x.Name.Contains(searchQuery));
                }
                return Success(query);
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<Product>>(ex.InnerException.Message);
            }
        }
        public async Task<ReturnBase<bool>> SaveRecentSearchResultAsync(RecentSearch recentSearch)
        {
            try
            {
                var saveRecentSearchResult = await _recentSearchRepository.AddAsync(recentSearch);
                if (!saveRecentSearchResult.Succeeded)
                    return Failed<bool>(saveRecentSearchResult.Message);
                return Success(true);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public ReturnBase<IQueryable<RecentSearch>> GetRecentSearchForUser(string userId)
        {
            try
            {
                var query = _recentSearchRepository.GetTableNoTracking()
                    .Data.Where(x => x.UserId == userId).AsQueryable();

                return Success(query);
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<RecentSearch>>(ex.InnerException.Message);
            }
        }
    }
}
