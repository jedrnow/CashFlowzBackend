namespace CashFlowzBackend.Infrastructure.Services
{
    public interface ICheckUserService
    {
        Task<bool> CheckUserExist(int userId);
    }
}