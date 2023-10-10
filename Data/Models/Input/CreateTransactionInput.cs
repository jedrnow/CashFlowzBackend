namespace CashFlowzBackend.Data.Models.Input
{
    public class CreateTransactionInput
    {
        public DateOnly Date { get; init; }
        public string? Description { get; init; }
        public List<decimal> Incomes { get; init; } = new List<decimal>();
        public List<decimal> Expenses { get; init; } = new List<decimal>();
    }
}
