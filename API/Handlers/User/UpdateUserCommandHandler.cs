using AutoMapper;
using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.API.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            User userToUpdate = await _userRepository.GetUserByIdToEdit(request.UserId);

            userToUpdate.Update(
                request.UpdateUserInput.Login,
                request.UpdateUserInput.Email,
                request.UpdateUserInput.FirstName,
                request.UpdateUserInput.LastName
                );

            return (await _userRepository.SaveChangesAsync());
        }
    }
}
