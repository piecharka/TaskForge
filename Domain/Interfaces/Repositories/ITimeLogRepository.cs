﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ITimeLogRepository
    {
        Task<TimeLog> GetTimeLogAsync(int timeLogId);
        Task<IEnumerable<TimeLog>> GetSprintOverdueTimeLogsAsync(int teamId);
        Task AddTimeLog(TimeLog timeLog);
        Task<IEnumerable<TimeLog>> GetSprintTimeLogByDateAsync(DateTime date, int sprintId, int timeLogTypeId);
        Task<TimeLog> GetTimeLogByDoneTaskIdAsync(int taskId);
    }
}
