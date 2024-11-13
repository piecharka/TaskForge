using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ISprintEventRepository
    {
        Task<IEnumerable<SprintEvent>> GetSprintEventsAsync();
        Task<IEnumerable<SprintEvent>> GetSprintEventsByTeamIdAsync(int teamId);
        Task<IEnumerable<SprintEvent>> GetSprintEventsByUserIdAsync(int userId);
        Task AddSprintEventAsync(SprintEvent sprintEvent);
        Task DeleteSprintEventAsync(int sprintEventId);
    }
}
