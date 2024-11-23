using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ITimeLogService
    {
        Task<TimeLog> GetTimeLogAsync(int timeLogId);
        Task<int> GetOverdueDeadlineCountAsync(int sprintId);
    }
}
