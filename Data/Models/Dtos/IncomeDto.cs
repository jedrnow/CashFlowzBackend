namespace CashFlowzBackend.Data.Models.Dtos
{
    public record IncomeDto
    {
        public int TransactionId { get; init; }
        public decimal Amount { get; init; }
    }
}
