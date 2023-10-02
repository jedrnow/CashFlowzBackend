using CashFlowzBackend.Infrastructure.Repositories;

namespace CashFlowzBackend.Infrastructure.Services
{
    public class CheckUserService : ICheckUserService
    {
        private readonly IUserRepository _userRepository;
        public CheckUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> CheckUserExist(int userId)
        {
            return await _userRepository.CheckUserExistsById(userId);
        }
    }
}
