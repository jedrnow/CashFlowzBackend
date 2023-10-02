using CashFlowzBackend.Data.Models.Dtos;
using MediatR;
using FluentValidation;

namespace CashFlowzBackend.API.Queries
{
    public record GetUsersListQuery: IRequest<List<UserDto>>
    {


        public GetUsersListQuery()
        {

        }
    }

    public class GetUsersListQueryValidator : AbstractValidator<GetUsersListQuery>
    {
        public GetUsersListQueryValidator()
        {

        }
    }
}
