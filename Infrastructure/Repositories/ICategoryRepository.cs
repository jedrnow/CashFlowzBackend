using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Data.Models.View;

namespace CashFlowzBackend.Infrastructure.Repositories
{
    public interface ICategoryRepository
    {
        Task AddCategory(Category category);

        Task DeleteCategory(int categoryId);

        Task<bool> CheckCategoryExistsById(int categoryId);

        Task<List<CategoryViewModel>> GetCategoryList();

        Task<Category> GetCategoryByIdToEdit(int categoryId);

        Task<bool> CheckCategoryHasActiveBudgets(int categoryId);

        Task<bool> SaveChangesAsync();
    }
}