using Application.DTOs;
using Domain;
using Domain.DTOs;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserGetDto>> GetUsersAsync();
        Task<UserGetDto> GetUserByIdAsync(int id);
        Task<UserGetDto> GetUserByNameAsync(string username);
        Task<UserGetDto> GetUserByEmailAsync(string email);
        Task InsertUserAsync(UserCreateDto user);
        Task UpdateUserPasswordAsync(UserUpdatePasswordDto user);
        Task UpdateUserEmailAsync(UserUpdateEmailDto user);
        Task DeleteUserAsync(int id); 
        Task<int> LoginUserAsync(UserLoginDto userLoginDto);
        Task<IEnumerable<UserGetDto>> GetTeamUsersAsync(int teamId);
    }
}
