using AutoMapper;
using CashFlowzBackend.Data.Models.Dtos;
using CashFlowzBackend.Data.Models.View;
using CashFlowzBackend.API.Queries;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionByIdQueryHandler(IMapper mapper, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            TransactionViewModel transactionById = await _transactionRepository.GetTransactionById(request.TransactionId);

            TransactionDto mappedResult = _mapper.Map<TransactionDto>(transactionById);

            return mappedResult;
        }
    }
}
