namespace CashFlowzBackend.Infrastructure.Services
{
    public interface ICheckBudgetService
    {
        Task<bool> CheckBudgetExist(int userId,int budgetId);
    }
}