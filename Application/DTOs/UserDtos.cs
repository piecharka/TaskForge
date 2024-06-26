﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserCreateDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public DateTime Birthday { get; set; }
    }
    public class UserUpdatePasswordDto
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; } = null!;
    }
    public class UserUpdateEmailDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
    }
    public class UserLoginDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
