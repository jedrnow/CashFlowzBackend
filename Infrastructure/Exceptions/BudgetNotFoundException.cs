namespace CashFlowzBackend.Infrastructure.Exceptions
{
    public class BudgetNotFoundException : Exception, ICustomException
    {
        public string Code { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }

        public BudgetNotFoundException(int budgetId)
        {
            Code = "404";
            Title = "BudgetNotFoundException";
            Description = $"Budget with id = {budgetId} was not found.";
        }
    }

}
