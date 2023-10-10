using CashFlowzBackend.Data.Models.View;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.Infrastructure.Repositories
{
    public interface IBudgetRepository
    {
        Task AddBudget(Budget budget);

        Task<bool> CheckUserBudgetExistsById(int userId, int budgetId);

        Task<List<BudgetViewModel>> GetUsersBudgetsList(int userId);

        Task<BudgetViewModel> GetBudgetById(int budgetId);

        Task<Budget> GetBudgetByIdToEdit(int budgetId);

        Task<bool> SaveChangesAsync();
    }
}
