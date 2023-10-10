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
    [Route("api/user/{userId}")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICheckBudgetService _checkBudgetService;
        private readonly ICheckUserService _checkUserService;
        private readonly ICheckTransactionService _checkTransactionService;

        public TransactionController(IMediator mediator, ICheckBudgetService checkBudgetService, ICheckUserService checkUserService, ICheckTransactionService checkTransactionService)
        {
            _mediator = mediator;
            _checkBudgetService = checkBudgetService;
            _checkUserService = checkUserService;
            _checkTransactionService = checkTransactionService;
        }

        [HttpPost("budget/{budgetId}/[controller]")]
        public async Task<ActionResult<int>> CreateTransaction(
            [FromRoute] int userId,
            [FromRoute] int budgetId,
            [FromBody] CreateTransactionInput input)
        {
            CreateTransactionCommand request = new(userId, budgetId, input);

            int result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("[controller]/list")]
        public async Task<ActionResult<List<TransactionDto>>> GetUserTransactionsList(
            [FromRoute] int userId)
        {
            await ValidateUserExists(userId);

            GetUserTransactionListQuery request = new(userId);

            List<TransactionDto> result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("budget/{budgetId}/[controller]/list")]
        public async Task<ActionResult<List<TransactionDto>>> GetBudgetTransactionsList(
            [FromRoute] int userId,
            [FromRoute] int budgetId)
        {
            await ValidateBudgetExists(userId, budgetId);

            GetBudgetTransactionListQuery request = new(userId, budgetId);

            List<TransactionDto> result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("budget/{budgetId}/[controller]/{transactionId}")]
        public async Task<ActionResult<TransactionDto>> GetTransactionById(
            [FromRoute] int userId,
            [FromRoute] int budgetId,
            [FromRoute] int transactionId)
        {
            await ValidateTransactionExists(userId, budgetId, transactionId);

            GetTransactionByIdQuery request = new(transactionId);

            TransactionDto result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("budget/{budgetId}/[controller]/{transactionId}")]
        public async Task<ActionResult<bool>> UpdateTransaction(
            [FromRoute] int userId,
            [FromRoute] int budgetId,
            [FromRoute] int transactionId,
            [FromBody] UpdateTransactionInput updateTransactionInput)
        {
            await ValidateTransactionExists(userId, budgetId, transactionId);

            UpdateTransactionCommand request = new(transactionId, updateTransactionInput);

            bool result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("budget/{budgetId}/[controller]/{transactionId}")]
        public async Task<ActionResult<bool>> DeleteTransaction(
            [FromRoute] int userId,
            [FromRoute] int budgetId,
            [FromRoute] int transactionId)
        {
            await ValidateTransactionExists(userId, budgetId, transactionId);

            DeleteTransactionCommand request = new(transactionId);

            bool result = await _mediator.Send(request);

            return Ok(result);
        }

        private async Task ValidateUserExists(int userId)
        {
            if (!await _checkUserService.CheckUserExist(userId))
            {
                throw new UserNotFoundException(userId);
            }
        }

        private async Task ValidateBudgetExists(int userId, int budgetId)
        {
            if (!await _checkUserService.CheckUserExist(userId))
            {
                throw new UserNotFoundException(userId);
            }

            if (!await _checkBudgetService.CheckBudgetExist(userId, budgetId))
            {
                throw new BudgetNotFoundException(budgetId);
            }
        }

        private async Task ValidateTransactionExists(int userId, int budgetId, int transactionId)
        {
            if (!await _checkUserService.CheckUserExist(userId))
            {
                throw new UserNotFoundException(userId);
            }

            if (!await _checkBudgetService.CheckBudgetExist(userId, budgetId))
            {
                throw new BudgetNotFoundException(budgetId);
            }

            if(!await _checkTransactionService.CheckTransactionExist(userId, budgetId, transactionId))
            {
                throw new TransactionNotFoundException(transactionId);
            }
        }
    }
}
