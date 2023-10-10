using AutoMapper;
using CashFlowzBackend.Data.Models.Dtos;
using CashFlowzBackend.Data.Models.View;
using CashFlowzBackend.API.Queries;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class GetUserTransactionListQueryHandler : IRequestHandler<GetUserTransactionListQuery, List<TransactionDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        public GetUserTransactionListQueryHandler(IMapper mapper, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionDto>> Handle(GetUserTransactionListQuery request, CancellationToken cancellationToken)
        {
            List<TransactionViewModel> userTransactionList = await _transactionRepository.GetUserTransactionList(request.UserId);

            List<TransactionDto> mappedResult = _mapper.Map<List<TransactionDto>>(userTransactionList);

            return mappedResult;
        }
    }
}
