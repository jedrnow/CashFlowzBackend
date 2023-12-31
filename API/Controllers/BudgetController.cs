﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICheckBudgetService _checkBudgetService;
        private readonly ICheckUserService _checkUserService;
         
        public BudgetController(IMediator mediator, ICheckBudgetService checkBudgetService, ICheckUserService checkUserService)
        {
            _mediator = mediator;
            _checkBudgetService = checkBudgetService;
            _checkUserService = checkUserService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBudget(
            [FromRoute] int userId,
            [FromBody] CreateBudgetInput input)
        {
            CreateBudgetCommand request = new(userId,input);

            int result = await _mediator.Send(request);

            return Ok(result);
        }


        [HttpGet("list")]
        public async Task<ActionResult<List<BudgetDto>>> GetBudgetsList(
            [FromRoute] int userId)
        {
            await ValidateUserExists(userId);

            GetUsersBudgetsListQuery request = new(userId);

            List<BudgetDto> result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("{budgetId}")]
        public async Task<ActionResult<BudgetDto>> GetBudgetById(
            [FromRoute] int userId,
            [FromRoute] int budgetId)
        {
            await ValidateBudgetExists(userId, budgetId);

            GetBudgetByIdQuery request = new(budgetId);

            BudgetDto result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("{budgetId}")]
        public async Task<ActionResult<bool>> UpdateBudget(
            [FromRoute] int userId,
            [FromRoute] int budgetId,
            [FromBody] UpdateBudgetInput updateBudgetInput)
        {
            await ValidateBudgetExists(userId, budgetId);

            UpdateBudgetCommand request = new(budgetId, updateBudgetInput);

            bool result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{budgetId}")]
        public async Task<ActionResult<bool>> DeleteBudget(
            [FromRoute] int userId,
            [FromRoute] int budgetId)
        {
            await ValidateBudgetExists(userId,budgetId);

            DeleteBudgetCommand request = new(budgetId);

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
            if(!await _checkUserService.CheckUserExist(userId))
            {
                throw new UserNotFoundException(userId);
            }

            if (!await _checkBudgetService.CheckBudgetExist(userId, budgetId))
            {
                throw new BudgetNotFoundException(budgetId);
            }
        }
    }
}
