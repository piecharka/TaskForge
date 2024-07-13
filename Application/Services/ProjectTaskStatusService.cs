using Application.Interfaces.Services;
using Domain;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProjectTaskStatusService : IProjectTaskStatusService
    {
        private readonly ITaskStatusRepository _taskStatusRepository;
        
        public ProjectTaskStatusService(ITaskStatusRepository taskStatusRepository)
        {
            _taskStatusRepository = taskStatusRepository;
        }

        public async Task DeleteTaskStatusAsync(int id)
        {
            await _taskStatusRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProjectTaskStatus>> GetAllTaskStatusAsync()
        {
            return await _taskStatusRepository.GetAllAsync();
        }

        public async Task<ProjectTaskStatus> GetTaskStatusByIdAsync(int id)
        {
            return await _taskStatusRepository.GetByIdAsync(id);
        }

        public async Task InsertTaskStatusAsync(ProjectTaskStatus projectTaskStatus)
        {
            await _taskStatusRepository.InsertAsync(projectTaskStatus);
        }

        public async Task UpdateTaskStatusAsync(ProjectTaskStatus projectTaskStatus)
        {
            await _taskStatusRepository.UpdateAsync(projectTaskStatus);
        }
    }
}
