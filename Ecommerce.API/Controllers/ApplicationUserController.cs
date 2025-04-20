using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/applicationuser/[action]")]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterApplicationUserCommand command)
        {
            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }
    }
}
