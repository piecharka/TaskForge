
using Domain;
using Persistence.DTOs;

namespace Application.Interfaces.Services
{
    public interface ITeamService
    {
        public Task<Team> GetTeamByIdAsync(int id);
        public Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        public Task InsertTeamAsync(TeamDto teamDto);
        public Task DeleteTeamAsync(int id);
        public Task AddUserToTeamAsync(string username, int teamId);
    }
}
