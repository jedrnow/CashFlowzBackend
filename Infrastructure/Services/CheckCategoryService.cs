using CashFlowzBackend.Infrastructure.Repositories;

namespace CashFlowzBackend.Infrastructure.Services
{
    public class CheckCategoryService : ICheckCategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CheckCategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> CheckCategoryExist(int categoryId)
        {
            return await _categoryRepository.CheckCategoryExistsById(categoryId);
        }

        public async Task<bool> CheckCategoryHasActiveBudgets(int categoryId)
        {
            return await _categoryRepository.CheckCategoryHasActiveBudgets(categoryId);
        }
    }
}
