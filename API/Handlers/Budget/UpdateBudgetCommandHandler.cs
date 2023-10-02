using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.API.Handlers
{
    public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, bool>
    {
        private readonly IBudgetRepository _budgetRepository;

        public UpdateBudgetCommandHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<bool> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
        {

            Budget budgetToUpdate = await _budgetRepository.GetBudgetByIdToEdit(request.BudgetId);

            budgetToUpdate.Update(
                request.UpdateBudgetInput.Name,
                request.UpdateBudgetInput.StartDate,
                request.UpdateBudgetInput.EndDate,
                request.UpdateBudgetInput.Goal,
                request.UpdateBudgetInput.CategoryId
                );

            return (await _budgetRepository.SaveChangesAsync());
        }
    }
}
