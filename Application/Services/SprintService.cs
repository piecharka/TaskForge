using Application.Interfaces.Services;
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

        public SprintService(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }

        public async Task<Sprint> GetSprintByIdAsync(int sprintId)
        {
            return await _sprintRepository.GetSprintByIdAsync(sprintId);
        }

        public async Task<SprintDto> GetCurrentSprintTeamAsync(int teamId)
        {
            return await _sprintRepository.GetCurrentTeamSprintAsync(teamId);
        }

        public async Task<IEnumerable<SprintDto>> GetSprintsAsync(int teamId)
        {
            return await _sprintRepository.GetTeamSprintsAsync(teamId);
        }
    }
}
