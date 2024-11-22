using Domain.Model;
using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ITeamRepository 
    {
        Task<IEnumerable<TeamDto>> GetAllAsync();
        Task<TeamDto> GetByIdAsync(int teamId);
        Task InsertAsync(Team team);
        Task UpdateAsync(Team team);
        Task DeleteAsync(int teamId);
        Task AddUserToTeamAsync(int teamId, User user, Permission permission);
        Task AddUsersToTeamAsync(int teamId, List<User> usersToAdd);
        Task<ICollection<TeamDto>> GetTeamsByUsername(string username);
        Task DeleteUserAsync(int userId, int teamId);
    }
}
