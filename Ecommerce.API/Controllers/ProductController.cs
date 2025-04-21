using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.ProductFeatuer.Command.Model;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/product/[action]")]
    public class ProductController : AppControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Add([FromForm] AddProductCommand command)
        {
            string? userIdFromToken = User.FindFirst("UserId")?.Value;
            string? userRoleFromToken = User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;


            command.UserId = userIdFromToken;
            command.UserRole = userRoleFromToken;

            ReturnBase<bool> response = await Mediator.Send(command);

            return ReturnResult(response);
        }
    }
}
