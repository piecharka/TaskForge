using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Persistence.Repositories;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IUserRepository repository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task DeleteUserAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<UserGetDto> GetUserByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<UserGetDto> GetUserByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<UserGetDto> GetUserByNameAsync(string username)
        {
            return await _repository.GetByNameAsync(username.ToLower());
        }

        public async Task<IEnumerable<UserGetDto>> GetUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<UserGetDto>> GetTeamUsersAsync(int teamId)
        {
            return await _repository.GetTeamUsersAsync(teamId);
        }

        public async Task InsertUserAsync(UserCreateDto userDto)
        {
            var user = _mapper.Map<UserCreateDto, User>(userDto);
            user.Username = user.Username.ToLower();
            user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.LastLogin = DateTime.Now;
            user.IsActive = true;
            await _repository.InsertAsync(user);
        }

        public async Task UpdateUserPasswordAsync(UserUpdatePasswordDto userDto)
        {
            var user = await _repository.GetWholeUserObjectByIdAsync(userDto.UserId);

            if(user == null)
            {
                throw new Exception("User not found");
            }

            user.PasswordHash = _passwordHasher.HashPassword(userDto.PasswordHash);
            user.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(user);
        }
        public async Task UpdateUserEmailAsync(UserUpdateEmailDto userDto)
        {
            var user = await _repository.GetWholeUserObjectByIdAsync(userDto.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Email = userDto.Email;
            user.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(user);
        }
    }
}
