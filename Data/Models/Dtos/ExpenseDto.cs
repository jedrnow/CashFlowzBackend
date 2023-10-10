namespace CashFlowzBackend.Data.Models.Dtos
{
    public record ExpenseDto
    {
        public int TransactionId { get; init; }
        public decimal Amount { get; init; }
    }
}
