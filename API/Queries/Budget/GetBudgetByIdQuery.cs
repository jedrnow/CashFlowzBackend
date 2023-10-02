using CashFlowzBackend.Data.Models.Dtos;
using MediatR;
using FluentValidation;

namespace CashFlowzBackend.API.Queries
{
    public record GetBudgetByIdQuery : IRequest<BudgetDto>
    {
        public int BudgetId { get; }

        public GetBudgetByIdQuery(int budgetId)
        {
            BudgetId= budgetId;
        }
    }

    public class GetBudgetByIdQueryValidator : AbstractValidator<GetBudgetByIdQuery>
    {
        public GetBudgetByIdQueryValidator()
        {

        }
    }
}
