namespace CashFlowzBackend.Infrastructure.Exceptions
{
    public class CategoryNotFoundException : Exception, ICustomException
    {
        public string Code { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }

        public CategoryNotFoundException(int categoryId)
        {
            Code = "404";
            Title = "CategoryNotFoundException";
            Description = $"Category with id = {categoryId} was not found.";
        }
    }

}
