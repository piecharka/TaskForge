using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime LastLogin { get; set; }

        public bool IsActive { get; set; }
        public int PermissionId { get; set; }
        public PermissionDto Permission { get; set; }
        public virtual ICollection<UserTeamDto> Teams { get; set; } = new List<UserTeamDto>();
    }

    public class UserTeamDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
