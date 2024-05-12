using Application.DTOs;
using Domain;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByNameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task InsertUserAsync(UserCreateDto user);
        Task UpdateUserPasswordAsync(UserUpdatePasswordDto user);
        Task UpdateUserEmailAsync(UserUpdateEmailDto user);
        Task DeleteUserAsync(int id);
        Task UpdateUserLoginAsync(int id);
    }
}
