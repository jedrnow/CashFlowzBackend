using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Infrastructure.Repositories;
using CashFlowzBackend.Infrastructure.Services;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICheckCategoryService _checkCategoryService;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ICheckCategoryService checkCategoryService)
        {
            _categoryRepository = categoryRepository;
            _checkCategoryService = checkCategoryService;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await ValidateCategory(request.CategoryId);

            Category categoryToDelete = await _categoryRepository.GetCategoryByIdToEdit(request.CategoryId);

            categoryToDelete.Delete();

            return (await _categoryRepository.SaveChangesAsync());
        }

        private async Task ValidateCategory(int categoryId)
        {
            bool categoryHasActiveBudgets = await _checkCategoryService.CheckCategoryHasActiveBudgets(categoryId);

            if (categoryHasActiveBudgets)
            {
                throw new Exception("Cannot delete active category.");
            }
        }
    }
}
