using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProjectTaskInsertDto
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int TaskStatusId { get; set; }

        public int TeamId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime TaskDeadline { get; set; }

        public int TaskTypeId { get; set; }

        public string TaskDescription { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

        public virtual User CreatedByNavigation { get; set; }

        public virtual ProjectTaskStatus TaskStatus { get; set; }

        public virtual ProjectTaskType TaskType { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<UsersTask> UsersTasks { get; set; } = new List<UsersTask>();
    }
}
