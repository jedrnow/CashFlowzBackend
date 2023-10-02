using MediatR;
using FluentValidation;
namespace CashFlowzBackend.API.Commands
{
    public record DeleteCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; }

        public DeleteCategoryCommand(int categoryId)
        {
            CategoryId = categoryId;
        }
    }

    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
        }
    }
}
