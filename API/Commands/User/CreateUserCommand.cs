using MediatR;
using FluentValidation;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.Infrastructure.Constants;
using System.Linq;

namespace CashFlowzBackend.API.Commands
{
    public record CreateUserCommand : IRequest<int>
    {
        public CreateUserInput CreateUserInput { get;}

        public CreateUserCommand(CreateUserInput createUserInput)
        {
            CreateUserInput = createUserInput;
        }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.CreateUserInput)
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
                    .RuleFor(input => input.Password)
                        .NotEmpty()
                        .Length(ValidationSettings.MinPasswordLength, ValidationSettings.MaxPasswordLength))
                 .ChildRules(x => x
                    .RuleFor(input => input.FirstName))
                        .NotEmpty()
                 .ChildRules(x => x
                    .RuleFor(input => input.LastName)
                        .NotEmpty());
        }
    }
}
