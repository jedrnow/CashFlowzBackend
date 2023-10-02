using MediatR;
using FluentValidation;
namespace CashFlowzBackend.API.Commands
{
    public record LogoutCommand : IRequest<bool>
    {
        public string Token { get; }

        public LogoutCommand(string token)
        {
            Token = token;
        }
    }

    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty();
        }
    }
}
