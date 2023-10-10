using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;

namespace CashFlowzBackend.API.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User userToDelete = await _userRepository.GetUserByIdToEdit(request.UserId);

            userToDelete.Delete();

            return (await _userRepository.SaveChangesAsync());
        }
    }
}
