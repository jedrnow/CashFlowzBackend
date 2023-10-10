using CashFlowzBackend.Data.Models.View;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.Infrastructure.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransaction(Transaction transaction);

        Task<bool> CheckUserTransactionExistsById(int userId, int budgetId, int transactionId);

        Task<List<TransactionViewModel>> GetUserTransactionList(int userId);

        Task<List<TransactionViewModel>> GetBudgetTransactionList(int userId, int budgetId);

        Task<TransactionViewModel> GetTransactionById(int transactionId);

        Task<Transaction> GetTransactionByIdToEdit(int transactionId);

        Task<bool> SaveChangesAsync();
    }
}
