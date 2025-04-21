using Ecommerce.API.Base;
using Ecommerce.Core.Featuers.ApplicationUserFeatuer.Command.Model;
using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginApplicationUserCommand command)
        {
            ReturnBase<string> response = await Mediator.Send(command);
            return ReturnResult(response);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            ReturnBase<string> response = await Mediator.Send(command);
            return ReturnResult(response);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            string? userIdFromToken = User.FindFirst("UserId")?.Value;

            if (userIdFromToken is null)
                return Unauthorized("Invalid Token");

            if (command.UserId != userIdFromToken)
                return Unauthorized("You are not allowed to perform this action");

            ReturnBase<bool> response = await Mediator.Send(command);

            return ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordEmail([FromBody] SendResetPasswordEmailCommand command)
        {
            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand command)
        {
            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateApplicationUserCommand command)
        {
            string? userIdFromToken = User.FindFirst("UserId")?.Value;

            if (userIdFromToken is null)
                return Unauthorized("Invalid Token");

            if (command.UserId != userIdFromToken)
                return Unauthorized("You are not allowed to perform this action");

            ReturnBase<bool> response = await Mediator.Send(command);
            return ReturnResult(response);
        }

    }
}
