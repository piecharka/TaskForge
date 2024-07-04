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
        Task<TeamDto> GetByIdAsync(int id);
        Task InsertAsync(Team user);
        Task UpdateAsync(Team user);
        Task DeleteAsync(int id);
        Task AddUserToTeamAsync(int id, User user);
        Task AddUsersToTeamAsync(int id, List<User> usersToAdd);
    }
}
