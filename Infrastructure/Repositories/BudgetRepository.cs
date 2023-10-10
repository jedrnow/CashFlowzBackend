using CashFlowzBackend.Data;
using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Data.Models.View;
using Microsoft.EntityFrameworkCore;

namespace CashFlowzBackend.Infrastructure.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly CFDbContext _context;

        public BudgetRepository(CFDbContext context)
        {
            _context = context;
        }

        public async Task AddBudget(Budget Budget)
        {
            await _context.Budgets.AddAsync(Budget);
        }

        public async Task<bool> CheckUserBudgetExistsById(int userId,int budgetId)
        {
            return (await _context.Budgets
                .Where(x=> x.UserId == userId && !x.User.Deleted)
                .SingleOrDefaultAsync(x => x.Id == budgetId && !x.Deleted)) != null;
        }

        public async Task<List<BudgetViewModel>> GetUsersBudgetsList(int userId)
        {
            return await _context.Budgets
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Where(x=>x.UserId==userId)
                .Select(x => new BudgetViewModel()
                {
                    Id=x.Id,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Goal = x.Goal,
                    Amount = x.Amount,
                    UserId = x.UserId,
                    CategoryId = x.CategoryId
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<BudgetViewModel> GetBudgetById(int budgetId)
        {
            return await _context.Budgets
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new BudgetViewModel()
                {
                    Id=x.Id,
                    Name=x.Name,
                    StartDate=x.StartDate,
                    EndDate=x.EndDate,
                    Goal=x.Goal,
                    Amount=x.Amount,
                    UserId=x.UserId,
                    CategoryId=x.CategoryId
                })
                .SingleOrDefaultAsync(x => x.Id == budgetId);
        }

        public async Task<Budget> GetBudgetByIdToEdit(int budgetId)
        {
            return await _context.Budgets
                .Where(x => !x.Deleted)
                .Include(x => x.User)
                .Include(x=>x.Transactions)
                    .ThenInclude(t=>t.Incomes)
                .Include(x=>x.Transactions)
                    .ThenInclude(t=>t.Expenses)
                .SingleOrDefaultAsync(x => x.Id == budgetId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
