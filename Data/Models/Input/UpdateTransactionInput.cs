namespace CashFlowzBackend.Data.Models.Input
{
    public class UpdateTransactionInput
    {
        public DateOnly Date { get; init; }
        public string? Description { get; init; }

    }
}
