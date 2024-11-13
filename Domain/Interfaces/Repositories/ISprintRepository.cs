using Domain.DTOs;
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
        Task<SprintDto> GetCurrentTeamSprintAsync(int teamId);
        Task<IEnumerable<SprintDto>> GetTeamSprintsAsync(int teamId);
        Task AddSprintAsync(Sprint sprint);
        Task DeleteSprintAsync(int sprintId);
    }
}
