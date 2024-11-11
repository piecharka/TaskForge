using Domain.Interfaces.Repositories;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class SprintEventRepository : ISprintEventRepository
    {
        private readonly TaskForgeDbContext _taskForgeDbContext;
        public SprintEventRepository(TaskForgeDbContext taskForgeDbContext)
        {
            _taskForgeDbContext = taskForgeDbContext;
        }

        public async Task<IEnumerable<SprintEvent>> GetSprintEventsAsync()
        {
            return await _taskForgeDbContext.SprintEvents
                .ToListAsync();
        }
        public async Task<IEnumerable<SprintEvent>> GetSprintEventsByTeamIdAsync(int teamId)
        {
            return await _taskForgeDbContext.SprintEvents
                .Where(e => e.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<IEnumerable<SprintEvent>> GetSprintEventsByUserIdAsync(int userId)
        {
            var userTeams = await _taskForgeDbContext.Users
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Teams)
                .ToListAsync();

            // Pobierz SprintEventy związane z zespołami użytkownika
            var sprintEvents = await _taskForgeDbContext.SprintEvents
                .Where(se => userTeams.Select(t => t.TeamId).Contains(se.TeamId))
                .ToListAsync();

            return sprintEvents;
        }

    }
}
