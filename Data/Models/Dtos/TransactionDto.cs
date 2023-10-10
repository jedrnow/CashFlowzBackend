namespace CashFlowzBackend.Data.Models.Dtos
{
    public record TransactionDto
    {
        public int Id { get; init; }
        public decimal Balance { get; init; }
        public string? Description { get; init; }
        public DateOnly Date { get; init; }
        public int UserId { get; init; }
        public int BudgetId { get; init; }
        public List<IncomeDto> Incomes { get; init; }
        public List<ExpenseDto> Expenses { get; init; }
    }
}
