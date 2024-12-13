using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TaskUserGetDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
