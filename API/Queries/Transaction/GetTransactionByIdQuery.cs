using CashFlowzBackend.Data.Models.Dtos;
using MediatR;
using FluentValidation;

namespace CashFlowzBackend.API.Queries
{
    public record GetTransactionByIdQuery : IRequest<TransactionDto>
    {
        public int TransactionId { get; }

        public GetTransactionByIdQuery(int transactionId)
        {
            TransactionId = transactionId;
        }
    }

    public class GetTransactionByIdQueryValidator : AbstractValidator<GetTransactionByIdQuery>
    {
        public GetTransactionByIdQueryValidator()
        {

        }
    }
}
