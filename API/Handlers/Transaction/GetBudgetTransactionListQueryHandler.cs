using AutoMapper;
using CashFlowzBackend.Data.Models.Dtos;
using CashFlowzBackend.Data.Models.View;
using CashFlowzBackend.API.Queries;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class GetBudgetTransactionListQueryHandler : IRequestHandler<GetBudgetTransactionListQuery, List<TransactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        public GetBudgetTransactionListQueryHandler(IMapper mapper, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionDto>> Handle(GetBudgetTransactionListQuery request, CancellationToken cancellationToken)
        {
            List<TransactionViewModel> budgetTransactionList = await _transactionRepository.GetBudgetTransactionList(request.UserId, request.BudgetId);

            List<TransactionDto> mappedResult = _mapper.Map<List<TransactionDto>>(budgetTransactionList);

            return mappedResult;
        }
    }
}
