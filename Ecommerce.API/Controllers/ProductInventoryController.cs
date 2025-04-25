using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.ProductInventoryFeatuer.Command.Model;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/productinventory/[action]")]
    public class ProductInventoryController : AppControllerBase
    {
        [HttpPut]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Update([FromBody] UpdateProductQuantityCommand command)
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
