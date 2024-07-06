using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain;
using Domain.Interfaces.Repositories;
using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public TeamService(ITeamRepository teamRepository, IMapper mapper, IUserService userService)
        {
            _teamRepository = teamRepository; 
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<TeamDto> GetTeamByIdAsync(int id)
        {
            return await _teamRepository.GetByIdAsync(id); 
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetAllAsync();
        }

        public async Task InsertTeamAsync(TeamDto teamDto)
        {
            var team = _mapper.Map<TeamDto, Team>(teamDto);
            await _teamRepository.InsertAsync(team);
        }

        public async Task DeleteTeamAsync(int id)
        {
            await _teamRepository.DeleteAsync(id);
        }

        public async Task AddUserToTeamAsync(string username, int teamId)
        {
            var user = await _userService.GetUserByNameAsync(username);
            var team = await GetTeamByIdAsync(teamId);

            if (user == null || team == null) return;

            await _teamRepository.AddUserToTeamAsync(teamId, user);
        }
    }
}
