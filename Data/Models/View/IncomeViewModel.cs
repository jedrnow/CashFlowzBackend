namespace CashFlowzBackend.Data.Models.View
{
    public record IncomeViewModel
    {
        public int TransactionId { get; init; }
        public decimal Amount { get; init; }
    }
}
