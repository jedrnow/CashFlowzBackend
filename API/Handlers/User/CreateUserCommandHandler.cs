using AutoMapper;
using CashFlowzBackend.API.Commands;
using CashFlowzBackend.Infrastructure.Repositories;
using MediatR;
using CashFlowzBackend.Data.Models;

namespace CashFlowzBackend.API.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string passwordHash = HashPassword(request.CreateUserInput.Password);

            User createdUser = new(
                request.CreateUserInput.Login,
                passwordHash,
                request.CreateUserInput.Email,
                request.CreateUserInput.FirstName,
                request.CreateUserInput.LastName
                );

            await _userRepository.AddUser(createdUser);
            await _userRepository.SaveChangesAsync();

            return createdUser.Id;
        }

        private string HashPassword(string plainTextPassword)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainTextPassword, salt);

            return hashedPassword;
        }
    }
}
