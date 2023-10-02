using MediatR;
using FluentValidation;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.Infrastructure.Constants;

namespace CashFlowzBackend.API.Commands
{
    public record UpdateUserCommand : IRequest<bool>
    {
        public int UserId { get; }
        public UpdateUserInput UpdateUserInput { get; }

        public UpdateUserCommand(int userId,UpdateUserInput updateUserInput)
        {
            UserId = userId;
            UpdateUserInput = updateUserInput;
        }
    }

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UpdateUserInput)
                .Cascade(CascadeMode.Stop)
                .ChildRules(x => x
                    .RuleFor(input => input.Email)
                        .NotEmpty()
                        .Length(ValidationSettings.MinEmailLength, ValidationSettings.MaxEmailLength)
                        .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex))
                 .ChildRules(x => x
                    .RuleFor(input => input.Login)
                        .NotEmpty()
                        .Length(ValidationSettings.MinLoginLength, ValidationSettings.MaxLoginLength))
                 .ChildRules(x => x
                    .RuleFor(input => input.FirstName))
                        .NotEmpty()
                 .ChildRules(x => x
                    .RuleFor(input => input.LastName)
                        .NotEmpty());
        }
    }
}
