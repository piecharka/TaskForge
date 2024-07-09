using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain;
using Domain.DTOs;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IMapper _mapper;
        public ProjectTaskService(IProjectTaskRepository projectTaskRepository, IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetAllProjectTasksInTeamAsync(int teamId)
        {
            return await _projectTaskRepository.GetAllInTeamAsync(teamId);
        }

        public async Task AddProjectTaskAsync(ProjectTaskInsertDto projectTaskInsert)
        {
            var projectTask = _mapper.Map<ProjectTaskInsertDto, ProjectTask>(projectTaskInsert);
            await _projectTaskRepository.InsertAsync(projectTask);
        }

        public async Task DeleteProjectTaskAsync(int id)
        {
            await _projectTaskRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskUserGetDto>> GetTaskUsersAsync(int taskId)
        {
            return await _projectTaskRepository.GetTaskUsersByTaskIdAsync(taskId);
        }
    }
}
