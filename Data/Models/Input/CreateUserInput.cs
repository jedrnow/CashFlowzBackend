namespace CashFlowzBackend.Data.Models.Input
{
    public class CreateUserInput
    {
        public string Login { get; init; }
        public string Password { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
    }
}
