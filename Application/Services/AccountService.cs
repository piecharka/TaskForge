using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain;
using Domain.Interfaces.Repositories;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        
        public AccountService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public async Task<User> LoginUserAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetWholeUserObjectByUsernameAsync(userLoginDto.Username);

            if (user != null && _passwordHasher.VerifyPassword(user.PasswordHash, userLoginDto.Password))
            {
                user.LastLogin = DateTime.Now;
                await _userRepository.UpdateAsync(user);

                return user;
            }
            else
                return null;
        }

        public async Task<User> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            var user = _mapper.Map<UserRegisterDto, User>(userRegisterDto);
            user.Username = user.Username.ToLower();
            user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.LastLogin = DateTime.Now;
            user.IsActive = true;
            await _userRepository.InsertAsync(user);

            return user;
        }

        public async Task<bool> ValidateUsernameAsync(string username)
        {
            var user = await _userRepository.GetByNameAsync(username);

            if (user == null) return true;
            return false;
        }

        public async Task<bool> ValidateEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null) return true;
            return false;
        }
    }
}
