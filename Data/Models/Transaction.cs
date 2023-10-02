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

            base.Delete();
        }
    }
}
