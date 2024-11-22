using Application.Interfaces.Services;
using Domain;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TimeLogService : ITimeLogService
    {
        private readonly ITimeLogRepository _timeLogRepository;
        public TimeLogService(ITimeLogRepository timeLogRepository)
        {
            _timeLogRepository = timeLogRepository;
        }

        public async Task<TimeLog> GetTimeLogAsync(int timeLogId)
        {
            return await _timeLogRepository.GetTimeLogAsync(timeLogId);
        }
    }
}
