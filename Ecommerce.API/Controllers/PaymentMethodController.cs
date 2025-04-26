using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Command.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/paymentmethod/[action]")]
    public class PaymentMethodController : AppControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] AddPaymentMethodCommand command)
        {
            var response = await Mediator.Send(command);
            return ReturnResult(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdatePaymentMethodCommand command)
        {
            var response = await Mediator.Send(command);
            return ReturnResult(response);
        }
    }
}
