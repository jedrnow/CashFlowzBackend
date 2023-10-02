using AutoMapper;
using CashFlowzBackend.Data.Models.Dtos;
using CashFlowzBackend.Data.Models.View;
using CashFlowzBackend.API.Queries;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, List<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUsersListQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            List<UserViewModel> usersList = await _userRepository.GetUsersList();

            List<UserDto> mappedResult = _mapper.Map<List<UserDto>>(usersList);

            return mappedResult;
        }
    }
}
