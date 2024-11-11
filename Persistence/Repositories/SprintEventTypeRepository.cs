using Domain;
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
    public class SprintEventTypeRepository : ISprintEventTypeRepository
    {
        private readonly TaskForgeDbContext _taskForgeDbContext;
        public SprintEventTypeRepository(TaskForgeDbContext context)
        {
            _taskForgeDbContext = context;
        }

        public async Task<IEnumerable<SprintEventType>> GetAllAsync()
        {
            return await _taskForgeDbContext.SprintEventTypes.ToListAsync();
        }
    }
}
