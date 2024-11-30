using Application.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISprintEventService
    {
        Task<IEnumerable<SprintEvent>> GetSprintEventsAsync();
        Task<IEnumerable<SprintEvent>> GetSprintEventsByTeamIdAsync(int teamId);
        Task<IEnumerable<SprintEvent>> GetSprintEventsByUserIdAsync(int userId);
        Task<IEnumerable<SprintEvent>> GetClosestThreeEventsAsync(int teamId);
        Task<SprintEvent> GetSprintEventByIdAsync(int eventId);
        Task AddSprintEventAsync(SprintEventDto sprintEventDto);
        Task DeleteSprintEventAsync(int sprintEventId);
    }
}
