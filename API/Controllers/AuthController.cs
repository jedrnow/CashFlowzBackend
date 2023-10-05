using Microsoft.AspNetCore.Mvc;
using MediatR;
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

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
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

            LogoutCommand request = new(token);

            bool result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
