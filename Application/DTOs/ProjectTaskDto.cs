using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProjectTaskDto
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int TaskStatusId { get; set; }

        public int TeamId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime TaskDeadline { get; set; }

        public int TaskTypeId { get; set; }

        public string TaskDescription { get; set; }

        public virtual TeamGetDto Team { get; set; }

        public virtual ICollection<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();

        public virtual ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();

        public virtual TaskUserGetDto CreatedByNavigation { get; set; }

        public virtual ProjectTaskStatusDto TaskStatus { get; set; }

        public virtual ProjectTaskTypeDto TaskType { get; set; }

        public virtual ICollection<TaskUserDto> UsersTasks { get; set; } = new List<TaskUserDto>();
        public virtual SprintGetDto Sprint { get; set; }
    }

    public class SprintGetDto
    {
        public int SprintId { get; set; }
        public string SprintName { get; set; }
        public DateTime SprintStart { get; set; }
        public DateTime SprintEnd { get; set; }
        public string GoalDescription { get; set; }
    }

    public class TaskUserDto
    {
        public int UserTaskId { get; set; }

        public int UserId { get; set; }

        public int TaskId { get; set; }
        public virtual TaskUserGetDto User { get; set; }
    }
}
