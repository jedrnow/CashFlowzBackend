using MediatR;
using FluentValidation;
using CashFlowzBackend.Data.Models.Input;

namespace CashFlowzBackend.API.Commands
{
    public record CreateBudgetCommand : IRequest<int>
    {
        public int UserId { get; }
        public CreateBudgetInput CreateBudgetInput { get; }

        public CreateBudgetCommand(int userId,CreateBudgetInput createBudgetInput)
        {
            UserId = userId;
            CreateBudgetInput = createBudgetInput;
        }
    }

    public class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
    {
        public CreateBudgetCommandValidator()
        {
            RuleFor(x => x.CreateBudgetInput)
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
