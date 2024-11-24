using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserTasksInsertDto
    {
        public int TaskId { get; set; }
        public List<int> UserIds { get; set; }

    }
}
