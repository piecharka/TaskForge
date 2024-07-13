using Domain.Interfaces.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;

namespace Application.Services
{
    public class ProjectTaskTypeService : IProjectTaskTypeService
    {
        private readonly ITaskTypeRepository _taskTypeRepository;

        public ProjectTaskTypeService(ITaskTypeRepository taskTypeRepository)
        {
            _taskTypeRepository = taskTypeRepository;
        }

        public async Task DeleteTaskTypeAsync(int id)
        {
            await _taskTypeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProjectTaskType>> GetAllTaskTypesAsync()
        {
            return await _taskTypeRepository.GetAllAsync();
        }

        public async Task<ProjectTaskType> GetTaskTypeByIdAsync(int id)
        {
            return await _taskTypeRepository.GetByIdAsync(id);
        }

        public async Task InsertTaskTypeAsync(ProjectTaskType projectTaskType)
        {
            await _taskTypeRepository.InsertAsync(projectTaskType);
        }

        public async Task UpdateTaskTypeAsync(ProjectTaskType projectTaskType)
        {
            await _taskTypeRepository.UpdateAsync(projectTaskType);
        }
    }
}
