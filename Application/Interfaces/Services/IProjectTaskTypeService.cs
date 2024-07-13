using Domain;
using Persistence.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProjectTaskTypeService
    {
        public Task<ProjectTaskType> GetTaskTypeByIdAsync(int id);
        public Task<IEnumerable<ProjectTaskType>> GetAllTaskTypesAsync();
        public Task InsertTaskTypeAsync(ProjectTaskType projectTaskType);
        public Task DeleteTaskTypeAsync(int id);
        public Task UpdateTaskTypeAsync(ProjectTaskType projectTaskType);
    }
}
