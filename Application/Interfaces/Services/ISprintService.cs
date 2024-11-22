using Domain.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISprintService
    {
        Task<Sprint> GetSprintByIdAsync(int sprintId);
        Task<SprintDto> GetCurrentSprintTeamAsync(int teamId);
        Task<SprintDto> GetPreviousSprintAsync(int teamId);
        Task<IEnumerable<SprintDto>> GetSprintsAsync(int teamId);
        Task AddSprintAsync(SprintDto sprintDto);
        Task DeleteSprintAsync(int sprintId);
    }
}
