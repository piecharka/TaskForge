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
    public class TimeLogRepository : ITimeLogRepository
    {
        private readonly TaskForgeDbContext _taskForgeDbContext;
        public TimeLogRepository(TaskForgeDbContext context)
        {
            _taskForgeDbContext = context;
        }
        
        public async Task<TimeLog> GetTimeLogAsync(int timeLogId)
        {
            return await _taskForgeDbContext.TimeLogs
                .Where(t => t.LogId == timeLogId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TimeLog>> GetSprintOverdueTimeLogsAsync(int sprintId)
        {
            return await _taskForgeDbContext.TimeLogs
                .Include(t => t.ProjectTask)
                .Where(t => t.ProjectTask.SprintId == sprintId && t.LogTypeId == 1)
                .ToListAsync();
        }
        
    }
}
