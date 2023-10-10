using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Data.Models.View;

namespace CashFlowzBackend.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);

        Task<bool> CheckUserExistsById(int userId);

        Task<List<UserViewModel>> GetUsersList();

        Task<UserViewModel> GetUserById(int userId);

        Task<User> GetUserByLogin(string login);

        Task<User> GetUserByIdToEdit(int userId);

        Task<bool> SaveChangesAsync();
    }
}