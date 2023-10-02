using AutoMapper;
using CashFlowzBackend.Data.Models.Dtos;
using CashFlowzBackend.Data.Models.View;
using CashFlowzBackend.API.Queries;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, BudgetDto>
    {
        private readonly IMapper _mapper;
        private readonly IBudgetRepository _budgetRepository;

        public GetBudgetByIdQueryHandler(IMapper mapper, IBudgetRepository budgetRepository)
        {
            _mapper = mapper;
            _budgetRepository = budgetRepository;
        }

        public async Task<BudgetDto> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
        {
            BudgetViewModel budgetById = await _budgetRepository.GetBudgetById(request.BudgetId);

            BudgetDto mappedResult = _mapper.Map<BudgetDto>(budgetById);

            return mappedResult;
        }
    }
}
