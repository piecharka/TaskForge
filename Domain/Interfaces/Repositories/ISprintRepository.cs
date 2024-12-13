
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ISprintRepository
    {
        Task<Sprint> GetSprintByIdAsync(int id);
        Task<Sprint> GetCurrentTeamSprintAsync(int teamId);
        Task<Sprint> GetPreviousTeamSprintAsync(int teamId);
        Task<IEnumerable<Sprint>> GetTeamSprintsAsync(int teamId);
        Task AddSprintAsync(Sprint sprint);
        Task DeleteSprintAsync(int sprintId);
    }
}
