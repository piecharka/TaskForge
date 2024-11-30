using Domain.DTOs;
using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IProjectTaskRepository
    {
        Task<IEnumerable<ProjectTaskDto>> GetAllInTeamAsync(int teamId);
        Task<ProjectTaskDto> GetByIdAsync(int id);
        Task InsertAsync(ProjectTask user, List<int> userIds);
        Task AddUsersToTaskAsync(int taskId, List<int> userIds);
        Task UpdateAsync(ProjectTask user);
        Task DeleteAsync(int teamId);
        Task<ICollection<TaskUserGetDto>> GetTaskUsersByTaskIdAsync(int taskId);
        Task<ICollection<ProjectTask>> GetAllTasksByUserIdAsync(int userId);
        Task<ICollection<ProjectTask>> GetAllTasksByUsernameAsync(string username);
        Task<ICollection<ProjectTaskDto>> GetAllTasksBySprintIdAsync(int sprintId);
        Task<ICollection<ProjectTask>> GetTasksAssignedInSprintAsync(int sprintId, int userId);
        Task UpdateProjectTaskStatusAsync(int taskId, int statusId);
    }
}
