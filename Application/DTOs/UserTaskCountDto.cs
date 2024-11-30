using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserTaskCountDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int TaskCount { get; set; }
    }
}
