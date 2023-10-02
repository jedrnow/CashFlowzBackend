namespace CashFlowzBackend.Data.Models.Input
{
    public class UpdateBudgetInput
    {
        public string Name { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly? EndDate { get; init; }
        public decimal? Goal { get; init; }
        public int CategoryId { get; init; }
    }
}
