namespace CashFlowzBackend.Data.Models.Dtos
{
    public record UserDto
    {
        public int Id { get; init; }
        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }
}
