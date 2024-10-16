using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProjectTaskDisplayDto
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int TaskStatusId { get; set; }

        public int TeamId { get; set; }

        public DateTime TaskDeadline { get; set; }

        public int TaskTypeId { get; set; }
    }
}
