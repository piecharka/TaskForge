﻿using Application.DTOs;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<User> LoginUserAsync(UserLoginDto userLoginDto);
        Task<User> RegisterUserAsync(UserRegisterDto userRegisterDto);
        Task<bool> ValidateEmailAsync(string email);
        Task<bool> ValidateUsernameAsync(string username);
    }
}
