using Domain;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UsersTaskRepository : IUsersTaskRepository
    {
        private readonly TaskForgeDbContext _taskForgeDbContext;
        public UsersTaskRepository(TaskForgeDbContext context) 
        {
            _taskForgeDbContext = context;
        }

        public async Task<IEnumerable<UsersTask>> GetUsersTaskByTaskIdAsync(int taskId)
        {
            return await _taskForgeDbContext.UsersTasks
                .ToListAsync();
        }

        public async Task DeleteUserTaskByIdAsync(int userTaskId)
        {
            var entity = await _taskForgeDbContext.UsersTasks
                .Where(t => t.UserTaskId == userTaskId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _taskForgeDbContext.UsersTasks.Remove(entity);
                await _taskForgeDbContext.SaveChangesAsync();
            }
            else
            {
                // Opcjonalnie, możesz rzucić wyjątek lub zwrócić odpowiedź, jeśli zespół nie istnieje
                throw new KeyNotFoundException($"Usertask with id {userTaskId} not found.");
            }
        }

        public async Task DeleteUserTaskAsync(int taskId, int userId)
        {
            var entity = await _taskForgeDbContext.UsersTasks
                .Where(t => t.TaskId == taskId && t.UserId == userId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _taskForgeDbContext.UsersTasks.Remove(entity);
                await _taskForgeDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Usertask with userId {userId} and taskId {taskId} not found.");
            }
        }

    }
}
