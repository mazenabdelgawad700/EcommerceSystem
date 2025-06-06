﻿using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Abstracts;
using Ecommerce.Service.Abstraction;
using Ecommerce.Shared.Base;

namespace Ecommerce.Service.Implementation
{
    internal class ReviewService : ReturnBaseHandler, IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            this._reviewRepository = reviewRepository;
        }

        public async Task<ReturnBase<bool>> AddReviewAsync(Review review)
        {
            try
            {
                var addResult = await _reviewRepository.AddAsync(review);
                return addResult.Succeeded ? Success(true) : Failed<bool>(addResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }

        public async Task<ReturnBase<bool>> DeleteReviewAsync(int reviewId)
        {
            try
            {
                var deleteResult = await _reviewRepository.DeleteAsync(reviewId);
                return deleteResult.Succeeded ? Success(true) : Failed<bool>(deleteResult.Message);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.InnerException.Message);
            }
        }
        public ReturnBase<IQueryable<Review>> GetProductReviewsAsync(int productId)
        {
            try
            {
                var getProductReviewsResult = _reviewRepository.GetTableNoTracking().Data.Where(x => x.ProductId == productId).AsQueryable();

                return getProductReviewsResult is not null ? Success(getProductReviewsResult) : Failed<IQueryable<Review>>("Can not get product reviews");
            }
            catch (Exception ex)
            {
                return Failed<IQueryable<Review>>(ex.InnerException.Message);
            }
        }
    }
}
