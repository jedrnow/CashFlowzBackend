using MediatR;
using FluentValidation;
namespace CashFlowzBackend.API.Commands
{
    public record DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; }

        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }
    }

    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
        }
    }
}
