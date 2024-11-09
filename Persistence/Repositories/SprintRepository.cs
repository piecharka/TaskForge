using Domain;
using Domain.DTOs;
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

        public async Task<SprintDto> GetCurrentTeamSprintAsync(int teamId)
        {
            return await _forgeDbContext.Sprints
                .Select(s => new SprintDto
                {
                    SprintId = s.SprintId,
                    SprintName = s.SprintName,
                    SprintStart = s.SprintStart,
                    SprintEnd = s.SprintEnd,
                    GoalDescription = s.GoalDescription,
                    TeamId = s.TeamId
                })
                .Where(s => s.TeamId == teamId && s.SprintStart <= DateTime.Now && s.SprintEnd >= DateTime.Now)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SprintDto>> GetTeamSprintsAsync(int teamId)
        {
            return await _forgeDbContext.Sprints
                .Select(s => new SprintDto
                {
                    SprintId = s.SprintId,
                    SprintName = s.SprintName,
                    SprintStart = s.SprintStart,
                    SprintEnd = s.SprintEnd,
                    GoalDescription = s.GoalDescription,
                    TeamId = s.TeamId
                })
                .Where(s => s.TeamId == teamId)
                .ToListAsync();
        }
    }
}
