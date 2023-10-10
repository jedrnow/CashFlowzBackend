using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.API.Handlers
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;

        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            Transaction transactionToDelete = await _transactionRepository.GetTransactionByIdToEdit(request.TransactionId);

            transactionToDelete.Delete();

            return (await _transactionRepository.SaveChangesAsync());
        }
    }
}
