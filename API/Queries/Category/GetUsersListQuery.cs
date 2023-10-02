using CashFlowzBackend.Data.Models.Dtos;
using MediatR;
using FluentValidation;

namespace CashFlowzBackend.API.Queries
{
    public record GetCategoryListQuery: IRequest<List<CategoryDto>>
    {


        public GetCategoryListQuery()
        {

        }
    }

    public class GetCategoryListQueryValidator : AbstractValidator<GetCategoryListQuery>
    {
        public GetCategoryListQueryValidator()
        {

        }
    }
}
