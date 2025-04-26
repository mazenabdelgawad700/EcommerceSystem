using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.OrderItemFeatuer.Command.Model;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/orderitem/[action]")]
    public class OrderItemController : AppControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddOrderItemCommand command)
        {
            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }
    }
}
