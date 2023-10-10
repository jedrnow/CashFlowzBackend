using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.API.Handlers
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;

        public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            Transaction transactionToUpdate =  await _transactionRepository.GetTransactionByIdToEdit(request.TransactionId);

            transactionToUpdate.Update(request.UpdateTransactionInput.Date, request.UpdateTransactionInput.Description);

            return (await _transactionRepository.SaveChangesAsync());
        }
    }
}
