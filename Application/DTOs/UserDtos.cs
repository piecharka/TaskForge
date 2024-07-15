namespace Application.DTOs
{
    public class UserCreateDto
    {
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
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UserLoginGetDto
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }

        public DateTime LastLogin { get; set; }

        public virtual ICollection<UserLoginTeamDto> Teams { get; set; } = new List<UserLoginTeamDto>();
    }
    public class UserLoginTeamDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }


    public class UserRegisterDto
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public DateTime Birthday { get; set; }
    }
}
