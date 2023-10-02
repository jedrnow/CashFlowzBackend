using Microsoft.AspNetCore.Mvc;
using MediatR;
using CashFlowzBackend.Infrastructure.Services;
using CashFlowzBackend.Data.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.API.Commands;

namespace CashFlowzBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICheckUserService _checkUserService;

        public AuthController(IMediator mediator, ICheckUserService checkUserService)
        {
            _mediator = mediator;
            _checkUserService = checkUserService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResultDto>> Login(
            [FromBody] LoginInput input)
        {
            LoginCommand request = new(input);

            LoginResultDto result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<bool>> Logout()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Check if the token is already revoked
            if (Startup.RevokedTokens.Contains(token))
            {
                return BadRequest(new { message = "Token already revoked" });
            }

            // Add the token to the blacklist
            Startup.RevokedTokens.Add(token);

            return Ok(new { message = "Logout successful" });

            LogoutCommand request = new(token);

            //bool result = await _mediator.Send(request);

            //return Ok(result);
        }

        private async Task ValidateUserExists(int userId)
        {
            if (!await _checkUserService.CheckUserExist(userId))
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
