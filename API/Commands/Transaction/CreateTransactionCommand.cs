using MediatR;
using FluentValidation;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.Infrastructure.Constants;

namespace CashFlowzBackend.API.Commands
{
    public record CreateTransactionCommand : IRequest<int>
    {
        public int UserId { get; }
        public int BudgetId { get; }
        public CreateTransactionInput CreateTransactionInput { get; }

        public CreateTransactionCommand(int userId, int budgetId, CreateTransactionInput createTransactionInput)
        {
            UserId = userId;
            BudgetId = budgetId;
            CreateTransactionInput = createTransactionInput;
        }
    }

    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.CreateTransactionInput)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .ChildRules(x => x.RuleFor(input => input.Date)
                        .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)))
                .ChildRules(x => x.RuleFor(input => input.Description)
                        .Length(ValidationSettings.MinDescriptionLength, ValidationSettings.MaxDescriptionLength)
                        .When(input => input.Description != null));
        }
    }
}
