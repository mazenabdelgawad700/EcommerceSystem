using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.TransactionFeatuer.Command.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/transaction/[action]")]
    public class TransactionController : AppControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] AddTransactionCommand command)
        {
            var response = await Mediator.Send(command);
            return ReturnResult(response);
        }
    }
}
