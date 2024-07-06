using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IProjectTaskRepository
    {
        Task<IEnumerable<ProjectTask>> GetAllInTeamAsync(int teamId);
        Task<ProjectTask> GetByIdAsync(int teamId);
        Task InsertAsync(ProjectTask user);
        Task UpdateAsync(ProjectTask user);
        Task DeleteAsync(int teamId);
    }
}
