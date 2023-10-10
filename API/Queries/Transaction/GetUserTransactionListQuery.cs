using CashFlowzBackend.Data.Models.Dtos;
using MediatR;
using FluentValidation;

namespace CashFlowzBackend.API.Queries
{
    public record GetUserTransactionListQuery : IRequest<List<TransactionDto>>
    {
        public int UserId { get; }

        public GetUserTransactionListQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetUserTransactionListQueryValidator : AbstractValidator<GetUserTransactionListQuery>
    {
        public GetUserTransactionListQueryValidator()
        {

        }
    }
}
