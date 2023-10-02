using MediatR;
using FluentValidation;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.Infrastructure.Constants;

namespace CashFlowzBackend.API.Commands
{
    public record CreateCategoryCommand : IRequest<int>
    {
        public CreateCategoryInput CreateCategoryInput { get; }

        public CreateCategoryCommand(CreateCategoryInput createCategoryInput)
        {
            CreateCategoryInput = createCategoryInput;
        }
    }

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CreateCategoryInput)
                .Cascade(CascadeMode.Stop)
                .ChildRules(x => x
                    .RuleFor(input => input.Name)
                        .NotEmpty()
                        .Length(ValidationSettings.MinNameLength, ValidationSettings.MaxNameLength));
        }
    }
}
