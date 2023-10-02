using MediatR;
using FluentValidation;
using CashFlowzBackend.Data.Models.Input;
using CashFlowzBackend.Data.Models.Dtos;

namespace CashFlowzBackend.API.Commands
{
    public record LoginCommand : IRequest<LoginResultDto>
    {
        public LoginInput LoginInput { get; }

        public LoginCommand(LoginInput loginInput)
        {
            LoginInput = loginInput;
        }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.LoginInput)
                .Cascade(CascadeMode.Stop)
                .ChildRules(x => x
                    .RuleFor(input => input.Login)
                    .NotEmpty())
                .ChildRules(x => x
                    .RuleFor(input => input.Password)
                    .NotEmpty());
        }
    }
}
