using Application.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SprintEventService : ISprintEventService
    {
        private readonly ISprintEventRepository _sprintEventRepository;

        public SprintEventService(ISprintEventRepository sprintEventRepository)
        {
            _sprintEventRepository = sprintEventRepository;
        }

        public async Task<IEnumerable<SprintEvent>> GetSprintEventsAsync()
        {
            return await _sprintEventRepository.GetSprintEventsAsync();
        }

        public async Task<IEnumerable<SprintEvent>> GetSprintEventsByTeamIdAsync(int teamId)
        {
            return await _sprintEventRepository.GetSprintEventsByTeamIdAsync(teamId);
        }

        public async Task<IEnumerable<SprintEvent>> GetSprintEventsByUserIdAsync(int userId)
        {
            return await _sprintEventRepository.GetSprintEventsByUserIdAsync(userId);
        }
    }
}
