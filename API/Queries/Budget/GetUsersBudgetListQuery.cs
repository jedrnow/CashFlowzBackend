using CashFlowzBackend.Data.Models.Dtos;
using MediatR;
using FluentValidation;

namespace CashFlowzBackend.API.Queries
{
    public record GetUsersBudgetsListQuery: IRequest<List<BudgetDto>>
    {
        public int UserId { get; }

        public GetUsersBudgetsListQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetUsersBudgetsListQueryValidator : AbstractValidator<GetUsersBudgetsListQuery>
    {
        public GetUsersBudgetsListQueryValidator()
        {

        }
    }
}
