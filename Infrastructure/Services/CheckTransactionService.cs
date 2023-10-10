using CashFlowzBackend.Infrastructure.Repositories;

namespace CashFlowzBackend.Infrastructure.Services
{
    public class CheckTransactionService : ICheckTransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public CheckTransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<bool> CheckTransactionExist(int userId,int budgetId, int transactionId)
        {
            return await _transactionRepository.CheckUserTransactionExistsById(userId, budgetId, transactionId);
        }
    }
}
