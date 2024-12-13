using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain;
using Domain.Interfaces.Repositories;
using Domain.Model;
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

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _repository.GetByEmailAsync(email);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetUserByNameAsync(string username)
        {
            var user = await _repository.GetByNameAsync(username.ToLower());

            return _mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<UserDto>> GetTeamUsersAsync(int teamId)
        {
            var teamUsers = await _repository.GetTeamUsersAsync(teamId);
            var dtos = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(teamUsers);

            foreach(var dto in dtos)
            {
                var user = teamUsers.First(u => u.UserId == dto.UserId);
                var teamUser = user.TeamUsers.FirstOrDefault(tu => tu.TeamId == teamId);
                if (teamUser?.Permission != null)
                {
                    dto.Permission = _mapper.Map<Permission, PermissionDto>(teamUser.Permission);
                }
            }

            return dtos;
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
