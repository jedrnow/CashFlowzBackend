namespace CashFlowzBackend.Infrastructure.Services
{
    public interface ICheckCategoryService
    {
        Task<bool> CheckCategoryExist(int categoryId);
        Task<bool> CheckCategoryHasActiveBudgets(int categoryId);
    }
}