namespace CashFlowzBackend.Infrastructure.Exceptions
{
    public interface ICustomException
    {
        public string Code { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }

    }
}
