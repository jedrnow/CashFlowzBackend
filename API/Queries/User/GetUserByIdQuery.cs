using CashFlowzBackend.Data.Models.Dtos;
using MediatR;
using FluentValidation;

namespace CashFlowzBackend.API.Queries
{
    public record GetUserByIdQuery : IRequest<UserDto>
    {
        public int UserId { get; }

        public GetUserByIdQuery(int userId)
        {
            UserId= userId;
        }
    }

    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {

        }
    }
}
