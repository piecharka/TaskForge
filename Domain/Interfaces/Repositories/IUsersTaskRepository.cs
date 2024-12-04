using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUsersTaskRepository
    {
        Task<IEnumerable<UsersTask>> GetUsersTaskByTaskIdAsync(int taskId);
        Task DeleteUserTaskByIdAsync(int userTaskId);
        Task DeleteUserTaskAsync(int taskId, int userId);
    }
}
