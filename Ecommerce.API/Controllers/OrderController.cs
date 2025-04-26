using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.OrderFeatuer.Ccommand.Model;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/order/[action]")]
    public class OrderController : AppControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddOrderCommand command)
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
