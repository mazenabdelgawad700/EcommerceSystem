using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.CartItemFeatuer.Model;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/cartitem/[action]")]
    public class CartItemController : AppControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddCartItemCommand command)
        {

            ReturnBase<bool> response = await Mediator.Send(command);

            return ReturnResult(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] DeleteCartItemCommand command)
        {

            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }
    }
}
