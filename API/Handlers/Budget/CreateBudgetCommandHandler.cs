using AutoMapper;
using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.API.Handlers
{
    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, int>
    {
        private readonly IBudgetRepository _budgetRepository;

        public CreateBudgetCommandHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<int> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            Budget createdBudget = new(
                request.CreateBudgetInput.Name,
                request.CreateBudgetInput.StartDate,
                request.CreateBudgetInput.EndDate,
                request.CreateBudgetInput.Goal,
                request.UserId,
                request.CreateBudgetInput.CategoryId
                );

            await _budgetRepository.AddBudget(createdBudget);
            await _budgetRepository.SaveChangesAsync();

            return createdBudget.Id;
        }
    }
}
