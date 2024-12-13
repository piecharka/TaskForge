using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserTaskDto
    {
        public int UserTaskId { get; set; }

        public int UserId { get; set; }

        public int TaskId { get; set; }

        public virtual ProjectTask Task { get; set; }
        public virtual TaskUserGetDto User { get; set; }
    }
}
