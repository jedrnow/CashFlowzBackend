namespace CashFlowzBackend.Infrastructure.Exceptions
{
    public class UserNotFoundException : Exception, ICustomException
    {
        public string Code { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }

        public UserNotFoundException(int userId)
        {
            Code = "404";
            Title = "UserNotFoundException";
            Description = $"User with id = {userId} was not found.";
        }
    }

}
