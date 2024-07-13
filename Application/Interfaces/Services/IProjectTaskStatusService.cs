using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProjectTaskStatusService
    {
        public Task<ProjectTaskStatus> GetTaskStatusByIdAsync(int id);
        public Task<IEnumerable<ProjectTaskStatus>> GetAllTaskStatusAsync();
        public Task InsertTaskStatusAsync(ProjectTaskStatus projectTaskStatus);
        public Task DeleteTaskStatusAsync(int id);
        public Task UpdateTaskStatusAsync(ProjectTaskStatus projectTaskStatus);
    }
}
