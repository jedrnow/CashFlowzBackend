using CashFlowzBackend.Data;
using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Data.Models.View;
using Microsoft.EntityFrameworkCore;

namespace CashFlowzBackend.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CFDbContext _context;

        public TransactionRepository(CFDbContext context)
        {
            _context = context;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public async Task<bool> CheckUserTransactionExistsById(int userId, int budgetId, int transactionId)
        {
            return (await _context.Transactions
                .Where(x => x.UserId == userId && !x.User.Deleted)
                .Where(x => x.Budget.Id == budgetId && !x.Budget.Deleted)
                .SingleOrDefaultAsync(x => x.Id == transactionId && !x.Deleted)) != null;
        }

        public async Task<List<TransactionViewModel>> GetUserTransactionList(int userId)
        {
            return await _context.Transactions
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Where(x => x.UserId == userId && !x.User.Deleted)
                .Select(x => new TransactionViewModel()
                {
                    Id = x.Id,
                    Balance = x.Balance,
                    Description = x.Description,
                    Date = x.Date,
                    UserId = x.UserId,
                    BudgetId = x.BudgetId,
                    Incomes = x.Incomes.Select(i=> new IncomeViewModel()
                    {
                        TransactionId = i.TransactionId,
                        Amount = i.Amount
                    }).ToList(),
                    Expenses = x.Expenses.Select(e=> new ExpenseViewModel()
                    {
                        TransactionId=e.TransactionId,
                        Amount = e.Amount
                    }).ToList()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<TransactionViewModel>> GetBudgetTransactionList(int userId, int budgetId)
        {
            return await _context.Transactions
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Where(x => x.UserId == userId && !x.User.Deleted)
                .Where(x=>x.BudgetId == budgetId && !x.Budget.Deleted)
                .Select(x => new TransactionViewModel()
                {
                    Id = x.Id,
                    Balance = x.Balance,
                    Description = x.Description,
                    Date = x.Date,
                    UserId = x.UserId,
                    BudgetId = x.BudgetId,
                    Incomes = x.Incomes.Select(i => new IncomeViewModel()
                    {
                        TransactionId = i.TransactionId,
                        Amount = i.Amount
                    }).ToList(),
                    Expenses = x.Expenses.Select(e => new ExpenseViewModel()
                    {
                        TransactionId = e.TransactionId,
                        Amount = e.Amount
                    }).ToList()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task<TransactionViewModel> GetTransactionById(int transactionId)
        {
            return await _context.Transactions
                .AsNoTracking()
                .Where(x => !x.Deleted && !x.User.Deleted && !x.Budget.Deleted)
                .Select(x => new TransactionViewModel()
                {
                    Id = x.Id,
                    Balance = x.Balance,
                    Description = x.Description,
                    Date = x.Date,
                    UserId = x.UserId,
                    BudgetId = x.BudgetId,
                    Incomes = x.Incomes.Select(i => new IncomeViewModel()
                    {
                        TransactionId = i.TransactionId,
                        Amount = i.Amount
                    }).ToList(),
                    Expenses = x.Expenses.Select(e => new ExpenseViewModel()
                    {
                        TransactionId = e.TransactionId,
                        Amount = e.Amount
                    }).ToList()
                })
                .SingleOrDefaultAsync(x => x.Id == transactionId);
        }

        public async Task<Transaction> GetTransactionByIdToEdit(int transactionId)
        {
            return await _context.Transactions
                .Where(x => !x.Deleted)
                .Include(x=>x.Budget)
                .Include(x=>x.Incomes)
                .Include(x=>x.Expenses)
                .SingleOrDefaultAsync(x => x.Id == transactionId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
