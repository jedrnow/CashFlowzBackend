namespace CashFlowzBackend.Infrastructure.Exceptions
{
    public class TransactionNotFoundException : Exception, ICustomException
    {
        public string Code { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }

        public TransactionNotFoundException(int transactionId)
        {
            Code = "404";
            Title = "TransactionNotFoundException";
            Description = $"Transaction with id = {transactionId} was not found.";
        }
    }

}
