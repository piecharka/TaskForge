using Application.Interfaces.Services;
using AutoMapper;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IMapper _mapper;

        public SprintService(ISprintRepository sprintRepository, IMapper mapper)
        {
            _sprintRepository = sprintRepository;
            _mapper = mapper;
        }

        public async Task<Sprint> GetSprintByIdAsync(int sprintId)
        {
            return await _sprintRepository.GetSprintByIdAsync(sprintId);
        }

        public async Task<SprintDto> GetCurrentSprintTeamAsync(int teamId)
        {
            return await _sprintRepository.GetCurrentTeamSprintAsync(teamId);
        }

        public async Task<SprintDto> GetPreviousSprintAsync(int teamId)
        {
            return await _sprintRepository.GetPreviousTeamSprintAsync(teamId);
        }

        public async Task<IEnumerable<SprintDto>> GetSprintsAsync(int teamId)
        {
            return await _sprintRepository.GetTeamSprintsAsync(teamId);
        }

        public async Task AddSprintAsync(SprintDto sprintDto)
        {
            var sprint = _mapper.Map<Sprint>(sprintDto);
            await _sprintRepository.AddSprintAsync(sprint);
        }

        public async Task DeleteSprintAsync(int sprintId)
        {
            await _sprintRepository.DeleteSprintAsync(sprintId);
        }
    }
}
