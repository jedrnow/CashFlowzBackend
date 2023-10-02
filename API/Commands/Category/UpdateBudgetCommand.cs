using MediatR;
using FluentValidation;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.Infrastructure.Constants;

namespace CashFlowzBackend.API.Commands
{
    public record UpdateCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; }
        public UpdateCategoryInput UpdateCategoryInput { get; }

        public UpdateCategoryCommand(int categoryId, UpdateCategoryInput updateCategoryInput)
        {
            CategoryId = categoryId;
            UpdateCategoryInput = updateCategoryInput;
        }
    }

    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.UpdateCategoryInput)
                .Cascade(CascadeMode.Stop)
                .ChildRules(x => x
                    .RuleFor(input => input.Name)
                        .NotEmpty()
                        .Length(ValidationSettings.MinNameLength,ValidationSettings.MaxNameLength));
        }
    }
}
