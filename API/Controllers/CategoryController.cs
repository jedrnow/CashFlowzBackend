using Microsoft.AspNetCore.Mvc;
using MediatR;
using CashFlowzBackend.Infrastructure.Services;
using CashFlowzBackend.API.Queries;
using CashFlowzBackend.Data.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.API.Commands;

namespace CashFlowzBackend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICheckCategoryService _checkCategoryService;

        public CategoryController(IMediator mediator, ICheckCategoryService checkCategoryService)
        {
            _mediator = mediator;
            _checkCategoryService = checkCategoryService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCategory(
            [FromBody] CreateCategoryInput input)
        {
            CreateCategoryCommand request = new(input);

            int result = await _mediator.Send(request);

            return Ok(result);
        }


        [HttpGet("list")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategoryList()
        {
            GetCategoryListQuery request = new();

            List<CategoryDto> result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("{categoryId}")]
        public async Task<ActionResult<bool>> UpdateCategory(
            [FromRoute] int categoryId,
            [FromBody] UpdateCategoryInput updateCategoryInput)
        {
            await ValidateCategoryExists(categoryId);

            UpdateCategoryCommand request = new(categoryId, updateCategoryInput);

            bool result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<bool>> DeleteCategory(
            [FromRoute] int categoryId)
        {
            await ValidateCategoryExists(categoryId);

            DeleteCategoryCommand request = new(categoryId);

            bool result = await _mediator.Send(request);

            return Ok(result);
        }
        private async Task ValidateCategoryExists(int categoryId)
        {
            if(!await _checkCategoryService.CheckCategoryExist(categoryId))
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
