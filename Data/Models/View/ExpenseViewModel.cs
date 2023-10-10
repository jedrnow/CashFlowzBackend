namespace CashFlowzBackend.Data.Models.View
{
    public record ExpenseViewModel
    {
        public int TransactionId { get; init; }
        public decimal Amount { get; init; }
    }
}
