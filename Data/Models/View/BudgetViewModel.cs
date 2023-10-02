namespace CashFlowzBackend.Data.Models.View
{
    public record BudgetViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly? EndDate { get; init; }
        public decimal? Goal { get; init; }
        public decimal Amount { get; init; }
        public int UserId { get; init; }
        public int CategoryId { get; init; }
    }
}
