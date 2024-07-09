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
    public class TaskTypeRepository : ITaskTypeRepository
    {
        private readonly TaskForgeDbContext _forgeDbContext;
        public TaskTypeRepository(TaskForgeDbContext forgeDbContext)
        {
            _forgeDbContext = forgeDbContext;
        }

        public async Task DeleteAsync(int taskTypeId)
        {
            var taskType = await _forgeDbContext.ProjectTaskTypes
                .FirstOrDefaultAsync(tt => tt.TypeId == taskTypeId);

            if (taskType != null)
            {
                _forgeDbContext.ProjectTaskTypes.Remove(taskType);
                await _forgeDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"ProjectTaskTypes with id {taskTypeId} not found.");
            }

        }

        public async Task<IEnumerable<ProjectTaskType>> GetAllAsync()
        {
            return await _forgeDbContext.ProjectTaskTypes.ToListAsync();
        }

        public async Task<ProjectTaskType> GetByIdAsync(int taskTypeId)
        {
            return await _forgeDbContext.ProjectTaskTypes
                .FirstOrDefaultAsync(tt => tt.TypeId == taskTypeId);
        }

        public async Task InsertAsync(ProjectTaskType taskType)
        {
            await _forgeDbContext.ProjectTaskTypes.AddAsync(taskType);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectTaskType taskType)
        {

            _forgeDbContext.ProjectTaskTypes
                .Update(taskType);
            await _forgeDbContext.SaveChangesAsync();
        }
    }
}
