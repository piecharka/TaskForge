
namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int userId);
        Task InsertAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int userId);
        Task<User> GetByNameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetWholeUserObjectByIdAsync(int userId);
        Task<User> GetWholeUserObjectByUsernameAsync(string username);
        Task<IEnumerable<User>> GetTeamUsersAsync(int teamId);
    }
}
