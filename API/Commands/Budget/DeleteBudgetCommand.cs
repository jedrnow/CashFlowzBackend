using MediatR;
using FluentValidation;
namespace CashFlowzBackend.API.Commands
{
    public record DeleteBudgetCommand : IRequest<bool>
    {
        public int BudgetId { get; }

        public DeleteBudgetCommand(int budgetId)
        {
            BudgetId = budgetId;
        }
    }

    public class DeleteBudgetCommandValidator : AbstractValidator<DeleteBudgetCommand>
    {
        public DeleteBudgetCommandValidator()
        {
        }
    }
}
