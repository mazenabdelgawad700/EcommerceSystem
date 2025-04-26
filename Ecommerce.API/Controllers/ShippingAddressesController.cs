using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.ShippingAddressesFeatuer.Command.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/shippginaddress/[action]")]
    public class ShippingAddressesController : AppControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddShippingAddressesCommand command)
        {
            var response = await Mediator.Send(command);
            return ReturnResult(response);
        }
    }
}
