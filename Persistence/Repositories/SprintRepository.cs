using Domain;

using Domain.Interfaces.Repositories;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class SprintRepository : ISprintRepository
    {
        private readonly TaskForgeDbContext _forgeDbContext;

        public SprintRepository(TaskForgeDbContext context)
        {
            _forgeDbContext = context;
        }

        public async Task<Sprint> GetSprintByIdAsync(int id)
        {
            return await _forgeDbContext.Sprints
                .Include(s => s.Team)
                .Include(s => s.ProjectTasks)
                .Where(s => s.SprintId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Sprint> GetCurrentTeamSprintAsync(int teamId)
        {
            return await _forgeDbContext.Sprints
                .Where(s => s.TeamId == teamId && s.SprintStart <= DateTime.Now && s.SprintEnd >= DateTime.Now)
                .FirstOrDefaultAsync();
        }

        public async Task<Sprint> GetPreviousTeamSprintAsync(int teamId)
        {
            return await _forgeDbContext.Sprints
                .Where(s => s.TeamId == teamId && s.SprintEnd < DateTime.Now)
                .OrderByDescending(s => s.SprintEnd)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Sprint>> GetTeamSprintsAsync(int teamId)
        {
            return await _forgeDbContext.Sprints
                .Include(s => s.ProjectTasks)
                .Where(s => s.TeamId == teamId)
                .ToListAsync();
        }

        public async Task AddSprintAsync(Sprint sprint)
        {
            await _forgeDbContext.Sprints.AddAsync(sprint);

            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task DeleteSprintAsync(int sprintId)
        {
            var entity = await _forgeDbContext.Sprints
                .Where(s => s.SprintId == sprintId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _forgeDbContext.Sprints.Remove(entity);
                await _forgeDbContext.SaveChangesAsync();
            }
            else
            {
                // Opcjonalnie, możesz rzucić wyjątek lub zwrócić odpowiedź, jeśli zespół nie istnieje
                throw new KeyNotFoundException($"Team with id {sprintId} not found.");
            }
        }
    }
}
