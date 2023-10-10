namespace CashFlowzBackend.Data.Models
{
    public class Transaction:BaseEntity
    {
        public decimal Balance { get; private set; }
        public string? Description { get; private set; }
        public DateOnly Date { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
        public int BudgetId { get;private set; }
        public Budget Budget { get; private set; }
        public virtual ICollection<Income> Incomes { get; private set; }
        public virtual ICollection<Expense> Expenses { get; private set;}

        public Transaction(decimal balance, string? description, DateOnly date, int userId,  int budgetId)
        {
            Balance = balance;
            Description = description;
            Date = date;
            UserId = userId;
            BudgetId = budgetId;
            Incomes = new List<Income>();
            Expenses = new List<Expense>();
        }

        public Transaction(decimal balance, string? description, DateOnly date, int userId, User user, int budgetId, ICollection<Income> incomes, ICollection<Expense> expenses)
        {
            Balance = balance;
            Description = description;
            Date = date;
            UserId = userId;
            User = user;
            BudgetId = budgetId;
            Incomes = incomes;
            Expenses = expenses;
        }

        public void AddIncome(Income newIncome)
        {
            Incomes.Add(newIncome);
        }

        public void AddExpense(Expense newExpense)
        {
            Expenses.Add(newExpense);
        }

        public void Update(DateOnly date, string? description)
        {
            Date = date;
            Description = description;
        }

        public void Delete()
        {
            foreach(Income income in Incomes)
            {
                income.Delete();
            }

            foreach (Expense expense in Expenses)
            {
                expense.Delete();
            }

            Budget.UpdateAmountOnDeleteTransaction(Balance);

            base.Delete();
        }
    }
}
