using Application.DTOs;
using Domain;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> GetUserByNameAsync(string username);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task InsertUserAsync(UserCreateDto user);
        Task UpdateUserPasswordAsync(UserUpdatePasswordDto user);
        Task UpdateUserEmailAsync(UserUpdateEmailDto user);
        Task DeleteUserAsync(int id); 
        Task<IEnumerable<UserDto>> GetTeamUsersAsync(int teamId);
    }
}
