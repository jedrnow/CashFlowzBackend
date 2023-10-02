using CashFlowzBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CashFlowzBackend.Data
{
    public class CFDbContext : DbContext
    {
        public CFDbContext(DbContextOptions<CFDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
