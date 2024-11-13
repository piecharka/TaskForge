using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public SprintEventService(ISprintEventRepository sprintEventRepository, IMapper mapper)
        {
            _sprintEventRepository = sprintEventRepository;
            _mapper = mapper;
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

        public async Task<IEnumerable<SprintEvent>> GetClosestThreeEventsAsync(int teamId)
        {
            var events = await _sprintEventRepository.GetSprintEventsByTeamIdAsync(teamId);
            
            return events
                .Where(e => e.SprintEventDate >= DateTime.Now)
                .OrderBy(e => Math.Abs((e.SprintEventDate - DateTime.Now).TotalDays))
                .Take(3);      
        }

        public async Task AddSprintEventAsync(SprintEventDto sprintEventDto)
        {
            var sprintEvent = _mapper.Map<SprintEventDto, SprintEvent>(sprintEventDto);
            await _sprintEventRepository.AddSprintEventAsync(sprintEvent);
        }

        public async Task DeleteSprintEventAsync(int sprintId)
        {
            await _sprintEventRepository.DeleteSprintEventAsync(sprintId);
        }
    }
}
