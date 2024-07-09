using Domain;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly TaskForgeDbContext _forgeDbContext;
        public TaskStatusRepository(TaskForgeDbContext forgeDbContext) 
        {
            _forgeDbContext = forgeDbContext;
        }

        public async Task DeleteAsync(int taskStatusId)
        {
            var taskStatus = await _forgeDbContext.ProjectTaskStatuses
                .FirstOrDefaultAsync(ts => ts.StatusId == taskStatusId);

            if (taskStatus != null)
            {
                _forgeDbContext.ProjectTaskStatuses.Remove(taskStatus);
                await _forgeDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"ProjectTaskStatus with id {taskStatusId} not found.");
            }

        }

        public async Task<IEnumerable<ProjectTaskStatus>> GetAllAsync()
        {
            return await _forgeDbContext.ProjectTaskStatuses.ToListAsync();
        }

        public async Task<ProjectTaskStatus> GetByIdAsync(int taskStatusId)
        {
            return await _forgeDbContext.ProjectTaskStatuses
                .FirstOrDefaultAsync(ts => ts.StatusId == taskStatusId);
        }

        public async Task InsertAsync(ProjectTaskStatus taskStatus)
        {
            await _forgeDbContext.ProjectTaskStatuses.AddAsync(taskStatus);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectTaskStatus taskStatus)
        {

            _forgeDbContext.ProjectTaskStatuses
                .Update(taskStatus);
            await _forgeDbContext.SaveChangesAsync();
        }
    }
}
