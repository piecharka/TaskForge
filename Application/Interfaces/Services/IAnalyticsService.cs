using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAnalyticsService
    {
        Task<int> AverageTasksCountPerSprint(int teamId);
        Task<int> AverageTaskCountPerUser(int teamId);
        Task<double> AverageTimeForTask(int teamId);
    }
}
