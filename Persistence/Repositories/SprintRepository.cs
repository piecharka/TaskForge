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
    }
}
