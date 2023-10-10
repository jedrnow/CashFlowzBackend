namespace CashFlowzBackend.Infrastructure.Services
{
    public interface ICheckTransactionService
    {
        Task<bool> CheckTransactionExist(int userId, int budgetId, int transactionId);
    }
}