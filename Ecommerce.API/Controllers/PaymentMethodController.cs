using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Command.Model;
using Ecommerce.Core.Featuers.PaymentMethodFeatuer.Query.Model;
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
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromBody] DeletePaymentMethodCommand command)
        {
            var response = await Mediator.Send(command);
            return ReturnResult(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate([FromBody] ActivatePaymentMethodCommand command)
        {
            var response = await Mediator.Send(command);
            return ReturnResult(response);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPaymentMethods([FromQuery] GetPaymentMethodsQuery query)
        {
            var response = await Mediator.Send(query);
            return ReturnResult(response);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetActivePaymentMethods()
        {
            var response = await Mediator.Send(new GetActivePaymentMethodsQuery());
            return ReturnResult(response);
        }
    }
}
