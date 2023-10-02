using CashFlowzBackend.Data;
using CashFlowzBackend.Data.Models;
using CashFlowzBackend.Data.Models.View;
using Microsoft.EntityFrameworkCore;

namespace CashFlowzBackend.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly CFDbContext _context;

        public UserRepository(CFDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteUser(int userId)
        {
            User? userToDelete = await _context.Users
                .Where(x => !x.Deleted)
                .SingleOrDefaultAsync(x => x.Id == userId);

            userToDelete!.Delete();
        }

        public async Task<bool> CheckUserExistsById(int userId)
        {
            return (await _context.Users.SingleOrDefaultAsync(x=>x.Id == userId && !x.Deleted)) != null;
        }

        public async Task<List<UserViewModel>> GetUsersList()
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                })
                .OrderBy(x=>x.LastName)
                .ToListAsync();
        }

        public async Task<UserViewModel> GetUserById(int userId)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                })
                .SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _context.Users
                .Where(x => !x.Deleted)
                .SingleOrDefaultAsync(x => x.Login == login);
        }

        public async Task<User> GetUserByIdToEdit(int userId)
        {
            return await _context.Users
                .Where(x => !x.Deleted)
                .SingleOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync())>0;
        }
    }
}
