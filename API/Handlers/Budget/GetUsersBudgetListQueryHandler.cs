using AutoMapper;
using CashFlowzBackend.Data.Models.Dtos;
using CashFlowzBackend.Data.Models.View;
using CashFlowzBackend.API.Queries;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class GetUsersBudgetsListQueryHandler : IRequestHandler<GetUsersBudgetsListQuery, List<BudgetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBudgetRepository _budgetRepository;

        public GetUsersBudgetsListQueryHandler(IMapper mapper, IBudgetRepository budgetRepository)
        {
            _mapper = mapper;
            _budgetRepository = budgetRepository;
        }

        public async Task<List<BudgetDto>> Handle(GetUsersBudgetsListQuery request, CancellationToken cancellationToken)
        {
            List<BudgetViewModel> usersBudgetsList = await _budgetRepository.GetUsersBudgetsList(request.UserId);

            List<BudgetDto> mappedResult = _mapper.Map<List<BudgetDto>>(usersBudgetsList);

            return mappedResult;
        }
    }
}
