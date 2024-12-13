
namespace Domain.Interfaces.Repositories
{
    public interface IProjectTaskRepository
    {
        Task<IEnumerable<ProjectTask>> GetAllInTeamAsync(int teamId);
        Task<ProjectTask> GetByIdAsync(int id);
        Task InsertAsync(ProjectTask user, List<int> userIds);
        Task AddUsersToTaskAsync(int taskId, List<int> userIds);
        Task UpdateAsync(ProjectTask user);
        Task DeleteAsync(int teamId);
        Task<ICollection<User>> GetTaskUsersByTaskIdAsync(int taskId);
        Task<ICollection<ProjectTask>> GetAllTasksByUserIdAsync(int userId);
        Task<ICollection<ProjectTask>> GetAllTasksByUsernameAsync(string username);
        Task<ICollection<ProjectTask>> GetAllTasksBySprintIdAsync(int sprintId);
        Task<ICollection<ProjectTask>> GetTasksAssignedInSprintAsync(int sprintId, int userId);
        Task UpdateProjectTaskStatusAsync(int taskId, int statusId);
    }
}
