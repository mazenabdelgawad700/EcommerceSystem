﻿using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.ReviewFeatuer.Command.Model;
using Ecommerce.Service.Abstraction.Model;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/review/[action]")]
    public class ReviewController : AppControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddReviewCommand command)
        {
            string? userIdFromToken = User.FindFirst("UserId")?.Value;

            if (userIdFromToken is null)
                return Unauthorized("Invalid Token");

            if (command.UserId != userIdFromToken)
                return Unauthorized("You are not allowed to perform this action");

            ReturnBase<bool> response = await Mediator.Send(command);

            return ReturnResult(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete([FromQuery] DeleteReviewCommand command)
        {
            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProductReviews([FromQuery] GetProductReviewsQuery query)
        {
            var response = await Mediator.Send(query);
            return ReturnResult(response);
        }
    }
}
