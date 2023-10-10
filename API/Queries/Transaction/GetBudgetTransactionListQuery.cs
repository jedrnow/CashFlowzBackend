using CashFlowzBackend.Data.Models.Dtos;
using MediatR;
using FluentValidation;

namespace CashFlowzBackend.API.Queries
{
    public record GetBudgetTransactionListQuery : IRequest<List<TransactionDto>>
    {
        public int UserId { get; }
        public int BudgetId { get; }

        public GetBudgetTransactionListQuery(int userId,int budgetId)
        {
            UserId = userId;
            BudgetId = budgetId;
        }
    }

    public class GetBudgetTransactionListQueryValidator : AbstractValidator<GetBudgetTransactionListQuery>
    {
        public GetBudgetTransactionListQueryValidator()
        {

        }
    }
}
