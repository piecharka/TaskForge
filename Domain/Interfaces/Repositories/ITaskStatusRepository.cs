using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ITaskStatusRepository
    {
        Task<IEnumerable<ProjectTaskStatus>> GetAllAsync();
        Task<ProjectTaskStatus> GetByIdAsync(int taskStatusId);
        Task InsertAsync(ProjectTaskStatus taskStatus);
        Task UpdateAsync(ProjectTaskStatus taskStatus);
        Task DeleteAsync(int taskStatusId);
    }
}
