using Microsoft.AspNetCore.Mvc;
using MediatR;
using CashFlowzBackend.Infrastructure.Services;
using CashFlowzBackend.API.Queries;
using CashFlowzBackend.Data.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Exceptions;

namespace CashFlowzBackend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICheckUserService _checkUserService;

        public UserController(IMediator mediator, ICheckUserService checkUserService)
        {
            _mediator = mediator;
            _checkUserService = checkUserService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<int>> CreateUser(
            [FromBody] CreateUserInput input)
        {
            CreateUserCommand request = new(input);

            int result = await _mediator.Send(request);

            return Ok(result);
        }


        [HttpGet("userList")]
        public async Task<ActionResult<List<UserDto>>> GetUsersList()
        {
            GetUsersListQuery request = new();

            List<UserDto> result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUserById(
            [FromRoute] int userId)
        {
            await ValidateUserExists(userId);

            GetUserByIdQuery request = new(userId);

            UserDto result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<bool>> UpdateUser(
            [FromRoute] int userId,
            [FromBody] UpdateUserInput updateUserInput)
        {
            await ValidateUserExists(userId);

            UpdateUserCommand request = new(userId, updateUserInput);

            bool result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<bool>> DeleteUser(
            [FromRoute] int userId)
        {
            await ValidateUserExists(userId);

            DeleteUserCommand request = new(userId);

            bool result = await _mediator.Send(request);

            return Ok(result);
        }
        private async Task ValidateUserExists(int userId)
        {
            if(!await _checkUserService.CheckUserExist(userId))
            {
                throw new UserNotFoundException(userId);
            }
        }
    }
}
