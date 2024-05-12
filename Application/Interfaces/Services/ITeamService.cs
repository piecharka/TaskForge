using Application.DTOs;
using Domain;

namespace Application.Interfaces.Services
{
    public interface ITeamService
    {
        public Task<Team> GetTeamByIdAsync(int id);
        public Task<IEnumerable<Team>> GetAllTeamsAsync();
        public Task InsertTeamAsync(TeamDto teamDto);
        public Task DeleteTeamAsync(int id);
    }
}
