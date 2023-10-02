using CashFlowzBackend.Infrastructure.Repositories;

namespace CashFlowzBackend.Infrastructure.Services
{
    public class CheckBudgetService : ICheckBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        public CheckBudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
        public async Task<bool> CheckBudgetExist(int userId, int budgetId)
        {
            return await _budgetRepository.CheckUserBudgetExistsById(userId,budgetId);
        }
    }
}
