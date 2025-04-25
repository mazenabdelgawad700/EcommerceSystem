using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.CartFeatuer.Command.Model;
using Ecommerce.Core.Featuers.CartFeatuer.Query.Model;
using Ecommerce.Shared.Base;
using Ecommerce.Shared.SharedResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/cart/[action]")]
    public class CartController : AppControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCart([FromQuery] GetCartQuery query)
        {
            string? userIdFromToken = User.FindFirst("UserId")?.Value;

            if (userIdFromToken is null)
                return Unauthorized("Invalid Token");

            if (query.UserId != userIdFromToken)
                return Unauthorized("You are not allowed to perform this action");

            ReturnBase<GetCartResponse> response = await Mediator.Send(query);

            return ReturnResult(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] DeleteCartCommand command)
        {
            string? userIdFromToken = User.FindFirst("UserId")?.Value;

            if (userIdFromToken is null)
                return Unauthorized("Invalid Token");

            if (command.UserId != userIdFromToken)
                return Unauthorized("You are not allowed to perform this action");

            ReturnBase<bool> response = await Mediator.Send(command);

            return ReturnResult(response);
        }
    }
}
