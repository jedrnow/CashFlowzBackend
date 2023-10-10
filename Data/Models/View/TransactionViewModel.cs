namespace CashFlowzBackend.Data.Models.View
{
    public record TransactionViewModel
    {
        public int Id { get; init; }
        public decimal Balance { get; init; }
        public string? Description { get; init; }
        public DateOnly Date { get; init; }
        public int UserId { get; init; }
        public int BudgetId { get; init; }
        public List<IncomeViewModel> Incomes { get; init; } = new List<IncomeViewModel>();
        public List<ExpenseViewModel> Expenses { get; init; } = new List<ExpenseViewModel>();
    }
}
