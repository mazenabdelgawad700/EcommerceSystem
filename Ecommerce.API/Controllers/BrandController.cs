using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.BrandFeatuer.Command.Model;
using Ecommerce.Core.Featuers.BrandFeatuer.Query.Model;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/brand/[action]")]
    public class BrandController : AppControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromForm] AddBrandCommand command)
        {
            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromForm] UpdateBrandCommand command)
        {
            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] DeleteBrandCommand command)
        {
            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBrandProducts([FromQuery] GetBrandProductsQuery query)
        {
            var response = await Mediator.Send(query);
            return ReturnResult(response);
        }
    }
}
