using MediatR;
using FluentValidation;
namespace CashFlowzBackend.API.Commands
{
    public record DeleteTransactionCommand : IRequest<bool>
    {
        public int TransactionId { get; }

        public DeleteTransactionCommand(int transactionId)
        {
            TransactionId = transactionId;
        }
    }

    public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionCommand>
    {
        public DeleteTransactionCommandValidator()
        {
        }
    }
}
