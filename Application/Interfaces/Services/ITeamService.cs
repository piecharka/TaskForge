
using Domain;
using Persistence.DTOs;

namespace Application.Interfaces.Services
{
    public interface ITeamService
    {
        public Task<TeamDto> GetTeamByIdAsync(int id);
        public Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        public Task InsertTeamAsync(TeamDto teamDto);
        public Task DeleteTeamAsync(int id);
        public Task AddUserToTeamAsync(string username, int teamId, int permissionId);
        Task<IEnumerable<TeamDto>> GetTeamsByUsernameAsync(string username);
    }
}
