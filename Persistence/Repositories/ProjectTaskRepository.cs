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
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly TaskForgeDbContext _forgeDbContext;
        public ProjectTaskRepository(TaskForgeDbContext forgeDbContext) 
        { 
            _forgeDbContext = forgeDbContext;
        }

        public async Task<IEnumerable<ProjectTask>> GetAllInTeamAsync(int teamId)
        {
            return await _forgeDbContext.ProjectTasks
                 .Include(t => t.Attachments) 
                 .Include(t => t.Comments)
                 .Include(t => t.CreatedByNavigation)
                 .Include(t => t.TaskStatus)
                 .Include(t => t.TaskType)
                 .Include(t => t.Team)
                 .Include(t => t.UsersTasks)
                 .Where(pt => pt.TeamId == teamId)
                 .ToListAsync();
        }

        public async Task<ProjectTask> GetByIdAsync(int id)
        {
            return await _forgeDbContext.ProjectTasks
                 .Include(pt => pt.Attachments) 
                 .Include(pt => pt.Comments)
                 .Include(pt => pt.CreatedByNavigation)
                 .Include(pt => pt.TaskStatus)
                 .Include(pt => pt.TaskType)
                 .Include(pt => pt.Team)
                 .Include(pt => pt.UsersTasks)
                 .Where(pt => pt.TaskId == id)
                 .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _forgeDbContext.ProjectTasks
                .Include(pt => pt.Attachments)
                .Include(pt => pt.Comments)
                .Include(pt => pt.CreatedByNavigation)
                .Include(pt => pt.TaskStatus)
                .Include(pt => pt.TaskType)
                .Include(pt => pt.Team)
                .Include(pt => pt.UsersTasks)
                .Where(t => t.TaskId == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _forgeDbContext.ProjectTasks.Remove(entity);
                await _forgeDbContext.SaveChangesAsync();
            }
            else
            {
                // Opcjonalnie, możesz rzucić wyjątek lub zwrócić odpowiedź, jeśli zespół nie istnieje
                throw new KeyNotFoundException($"ProjectTask with id {id} not found.");
            }
        }

        public async Task InsertAsync(ProjectTask projectTask)
        {
            await _forgeDbContext.ProjectTasks
                .AddAsync(projectTask);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProjectTask projectTask)
        {
            _forgeDbContext
                .Update(projectTask);

            await _forgeDbContext.SaveChangesAsync();
        }
    }
}
