using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.ProductFeatuer.Command.Model;
using Ecommerce.Core.Featuers.ProductFeatuer.Query.Model;
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
        [HttpPut]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Update([FromForm] UpdateProductCommand command)
        {
            string? userIdFromToken = User.FindFirst("UserId")?.Value;

            if (userIdFromToken is null)
                return Unauthorized("Invalid Token");

            if (command.UserId != userIdFromToken)
                return Unauthorized("You are not allowed to perform this action");

            ReturnBase<bool> response = await Mediator.Send(command);

            return ReturnResult(response);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
        {
            string? userIdFromToken = User.FindFirst("UserId")?.Value;

            if (userIdFromToken is null)
                return Unauthorized("Invalid Token");

            if (command.SellerId != userIdFromToken)
                return Unauthorized("You are not allowed to perform this action");

            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetProductByIdQuery query)
        {
            var response = await Mediator.Send(query);
            return ReturnResult(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductAsPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return ReturnResult(response);
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchAboutProductQuery query)
        {
            var response = await Mediator.Send(query);
            return ReturnResult(response);
        }
    }
}
