using MediatR;
using FluentValidation;
using CashFlowzBackend.Data.Models.Input;

namespace CashFlowzBackend.API.Commands
{
    public record UpdateBudgetCommand : IRequest<bool>
    {
        public int BudgetId { get; }
        public UpdateBudgetInput UpdateBudgetInput { get; }

        public UpdateBudgetCommand(int budgetId, UpdateBudgetInput updateBudgetInput)
        {
            BudgetId = budgetId;
            UpdateBudgetInput = updateBudgetInput;
        }
    }

    public class UpdateBudgetCommandValidator : AbstractValidator<UpdateBudgetCommand>
    {
        public UpdateBudgetCommandValidator()
        {
            RuleFor(x => x.UpdateBudgetInput)
                .Cascade(CascadeMode.Stop)
                .ChildRules(x => x
                    .RuleFor(input => input.Name)
                        .NotEmpty())
                 .ChildRules(x => x
                    .RuleFor(input => input.CategoryId)
                        .NotEmpty())
                 .ChildRules(x => x
                    .RuleFor(input => input.StartDate)
                        .NotEmpty())
                 .ChildRules(x => x
                    .RuleFor(input => input.EndDate)
                        .Cascade(CascadeMode.Stop)
                        .Must((input, endDate) => endDate == null || endDate > input.StartDate)
                        .WithMessage("End date must be later than the start date."));
        }
    }
}
