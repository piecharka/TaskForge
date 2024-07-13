using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
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

        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<TaskAttachmentsDto> Attachments { get; set; } = new List<TaskAttachmentsDto>();

        public virtual ICollection<TaskCommentDto> Comments { get; set; } = new List<TaskCommentDto>();

        public virtual TaskUserGetDto CreatedByNavigation { get; set; } 

        public virtual ProjectTaskStatus TaskStatus { get; set; }

        public virtual ProjectTaskType TaskType { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<UserTaskDto> UsersTasks { get; set; } = new List<UserTaskDto>();
    }

    public class TaskAttachmentsDto
    {
        public int AttachmentId { get; set; }

        public int TaskId { get; set; }

        public int AddedBy { get; set; }

        public string FilePath { get; set; }
    }

    public class TaskCommentDto
    {
        public int CommentId { get; set; }

        public int TaskId { get; set; }

        public int WrittenBy { get; set; }

        public string CommentText { get; set; }

        public DateTime WrittenAt { get; set; }
    }
    public class TaskUserGetDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime LastLogin { get; set; }

        public bool IsActive { get; set; }
    }
    public class UserTaskDto
    {
        public int UserTaskId { get; set; }

        public int UserId { get; set; }

        public int TaskId { get; set; }

        public virtual ProjectTask Task { get; set; }

        public virtual ICollection<TimeLogDto> TimeLogs { get; set; } = new List<TimeLogDto>();

        public virtual TaskUserGetDto User { get; set; }
    }
    
    public class TimeLogDto
    {
        public int LogId { get; set; }

        public int UserTaskId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
    
}
