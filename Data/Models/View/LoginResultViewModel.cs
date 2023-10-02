namespace CashFlowzBackend.Data.Models.View
{
    public record LoginResultViewModel
    {
        public int UserId { get; init; }
        public string Token { get; init; }
    }
}
