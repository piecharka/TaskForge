using Domain;
using Domain.DTOs;


namespace Application.DTOs
{
    public class UsersTasksToDoDto
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int TaskStatusId { get; set; }


        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime TaskDeadline { get; set; }

        public int TaskTypeId { get; set; }

        public string TaskDescription { get; set; }

        public TeamGetDto Team{ get; set; }
        public ProjectTaskStatus TaskStatus { get; set; }
    }
}
