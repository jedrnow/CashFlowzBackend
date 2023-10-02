namespace CashFlowzBackend.Data.Models.Dtos
{
    public record LoginResultDto
    {
        public int UserId { get; init; }
        public string Token { get; init; }
    }
}
