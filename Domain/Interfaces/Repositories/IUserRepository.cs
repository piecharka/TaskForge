using Domain.DTOs;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserGetDto>> GetAllAsync();
        Task<UserGetDto> GetByIdAsync(int userId);
        Task InsertAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int userId);
        Task<UserGetDto> GetByNameAsync(string username);
        Task<UserGetDto> GetByEmailAsync(string email);
        Task<User> GetWholeUserObjectByIdAsync(int userId);
        Task<User> GetWholeUserObjectByUsernameAsync(string username);
        Task<IEnumerable<UserGetDto>> GetTeamUsersAsync(int teamId);
    }
}
