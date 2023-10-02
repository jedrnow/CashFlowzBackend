using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, bool>
    {
        private readonly IBudgetRepository _budgetRepository;

        public DeleteBudgetCommandHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<bool> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
        {
            await _budgetRepository.DeleteBudget(request.BudgetId);

            return (await _budgetRepository.SaveChangesAsync());
        }
    }
}
